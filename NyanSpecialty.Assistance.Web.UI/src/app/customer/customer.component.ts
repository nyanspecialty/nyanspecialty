import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { LoaderService } from '../services/loader.service';
import { ToastrService } from 'ngx-toastr';
import { CustomerService } from '../services/customers.service';
import { Customers } from '../models/customers';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AddEditCustomerComponent } from './add-edit-customer.component';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { CommonPropsService } from '../services/common.service';

@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [CommonModule,
    FormsModule,
    AddEditCustomerComponent,
    GridHeaderComponent,
    CopyconfirmComponent,
    DeleteconfirmComponent,
    NgxDatatableModule],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css',
  encapsulation: ViewEncapsulation.None,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class CustomerComponent implements OnInit {

  customers: Customers[] = [];

  selectedItems: Customers[] = [];

  filteredRows: Customers[] = [];

  @ViewChild(AddEditCustomerComponent) addEditCustomerComponent!: AddEditCustomerComponent;

  isItemSelected: boolean = false;

  showDeleteConfirmModal: boolean = false;

  showCopyConfirmModal: boolean = false;

  headerCheckboxChecked: boolean = false;

  selectedCustomer: Customers = {} as Customers;

  newCustomer: Customers = {} as Customers;

  constructor(private loaderSerivce: LoaderService,
    private tosterService: ToastrService,
    private customerService: CustomerService,
    private commonPropsService: CommonPropsService
  ) {

  }
  ngOnInit() {
    this.loaderSerivce.showLoader();
    this.fecthCustomersAsync();
  }
  fecthCustomersAsync() {
    this.customerService.fetchCustomersAsync().subscribe(response => {
      this.customers = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as Customers[];
      this.filteredRows = this.customers;
      this.loaderSerivce.hideLoader();
    });
  }
  onAdd() {
    console.log('Add button clicked in GridHeader');
    this.addEditCustomerComponent.customer = this.newCustomer;
    this.addEditCustomerComponent.showSidebar();
  }

  onEdit() {
    console.log('Edit button clicked in GridHeader');
    this.isItemSelected = false;
    this.addEditCustomerComponent.customer = this.selectedCustomer;
    this.addEditCustomerComponent.showSidebar();
    this.selectedCustomer = {} as Customers;
  }
  onCloseSidebar() {
    this.addEditCustomerComponent.customer = this.newCustomer;
    this.addEditCustomerComponent.hideSideBar();
    this.selectedCustomer = {} as Customers;
    this.isItemSelected = false;
  }

  onDelete() {
    this.showDeleteConfirmModal = true;
  }
  confirmDelete() {
    this.loaderSerivce.showLoader();
    this.selectedCustomer.isActive = false;
    this.insertOrUpdateCustomerAsync(this.commonPropsService.prepareModelForSave(this.selectedCustomer), false, false, false, true);

  }
  cancelDelete() {
    this.showDeleteConfirmModal = false;
  }
  onCopy() {
    this.showCopyConfirmModal = true;
  }
  confirmCopy() {
    this.loaderSerivce.showLoader();
    this.selectedCustomer.customerID = 0;
    this.insertOrUpdateCustomerAsync(this.commonPropsService.prepareModelForSave(this.selectedCustomer), false, false, true, false);
  }
  cancelCopy() {
    this.showCopyConfirmModal = false;
  }
  onImport() {

  }
  onExportTemplate() {

  }
  onExportWithGridData() {

  }
  onExportWithOriginalData() {

  }
  toggleAll(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.customers.forEach(row => row.isChecked = isChecked);
    if (isChecked) {
      this.selectedItems = this.customers;
    } else {
      this.selectedItems = [];
    }
  }

  onRowCheckboxChange(row: any, event: any) {
    const allChecked = this.customers.every(row => row.isChecked);
    const anyUnchecked = this.customers.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
    if (row.isChecked) {
      this.selectedCustomer = row;
      this.isItemSelected = true;
      const multipleCustomers: Customers[] = [];
      multipleCustomers.push(row);
      this.addOrUpdateCustomers(multipleCustomers);
    } else {
      this.isItemSelected = false;
      this.selectedCustomer = {} as Customers;
      this.selectedItems = this.selectedItems.filter(item => item.customerID !== row.customerID);
    }
  }

  isAllChecked() {
    return this.customers.every(row => row.isChecked);
  }
  addOrUpdateCustomers(items: Customers[]): void {
    items.forEach(item => {
      const index = this.selectedItems.findIndex(existingItem => existingItem.customerID === item.customerID);
      if (index !== -1) {
        this.selectedItems[index] = { ...this.selectedItems[index], ...item };
      } else {
        this.selectedItems.push(item);
      }
    });
  }
  handleCustomerChange(customer: Customers) {
    console.log(customer);
    this.loaderSerivce.showLoader();
    const isAdd = customer.customerID === 0;
    const isUpdate = customer.customerID > 0;
    customer.isActive = true;
    this.insertOrUpdateCustomerAsync(this.commonPropsService.prepareModelForSave(customer), isAdd, isUpdate, false, false);

  }
  insertOrUpdateCustomerAsync(customer: Customers, isAdd: boolean, isUpdate: boolean, isCopy: boolean, isDelete: boolean) {
    this.customerService.insertOrUpdateCustomerAsync(customer).subscribe(response => {
      if (response) {
        if (isAdd)
          this.tosterService.success("role added sucessful");
        else if (isUpdate)
          this.tosterService.success("role updated sucessful");
        else if (isCopy) {
          this.isItemSelected = false;
          this.selectedCustomer = {} as Customers;
          this.showCopyConfirmModal = false;
          this.tosterService.success("role copied sucessful");
        }
        else if (isDelete) {
          this.isItemSelected = false;
          this.selectedCustomer = {} as Customers;
          this.showDeleteConfirmModal = false;
          this.tosterService.success("role deleted sucessful");
        }
      }
      this.fecthCustomersAsync();
      this.loaderSerivce.hideLoader();
    });
  }
}
