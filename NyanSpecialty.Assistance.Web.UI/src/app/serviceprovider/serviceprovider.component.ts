import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ServiceProvider } from '../models/serviceprovider';
import { AddEditServiceProviderComponent } from './add-edit-serviceprovider.component';
import { LoaderService } from '../services/loader.service';
import { ToastrService } from 'ngx-toastr';
import { ServiceProviderService } from '../services/serviceprovider.service';
import { CommonPropsService } from '../services/common.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@Component({
  selector: 'app-serviceprovider',
  standalone: true,
  imports: [CommonModule,
    FormsModule,
    AddEditServiceProviderComponent,
    GridHeaderComponent,
    CopyconfirmComponent,
    DeleteconfirmComponent,
    NgxDatatableModule],
  templateUrl: './serviceprovider.component.html',
  styleUrl: './serviceprovider.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ServiceProviderComponent implements OnInit {
  serviceProviders: ServiceProvider[] = [];

  selectedItems: ServiceProvider[] = [];

  filteredServiceProviders: ServiceProvider[] = [];

  @ViewChild(AddEditServiceProviderComponent) addEditServiceProviderComponent!: AddEditServiceProviderComponent;

  isItemSelected: boolean = false;

  showDeleteConfirmModal: boolean = false;

  showCopyConfirmModal: boolean = false;

  headerCheckboxChecked: boolean = false;

  selectedServiceProvider: ServiceProvider = {} as ServiceProvider;

  newServiceProvider: ServiceProvider = {} as ServiceProvider;

  constructor(private loaderSerivce: LoaderService,
    private tosterService: ToastrService,
    private serviceProviderService: ServiceProviderService,
    private commonPropsService: CommonPropsService
  ) {

  }
  ngOnInit() {
    this.loaderSerivce.showLoader();
    this.fecthServiceProvidersAsync();
  }
  fecthServiceProvidersAsync() {
    this.serviceProviderService.fetcherviceProvidersAsync().subscribe(response => {
      this.serviceProviders = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as ServiceProvider[];
      this.filteredServiceProviders = this.serviceProviders;
      this.loaderSerivce.hideLoader();
    });
  }
  onAdd() {
    console.log('Add button clicked in GridHeader');
    this.addEditServiceProviderComponent.serviceProvider = this.newServiceProvider;
    this.addEditServiceProviderComponent.showSidebar();
  }

  onEdit() {
    console.log('Edit button clicked in GridHeader');
    this.isItemSelected = false;
    this.addEditServiceProviderComponent.serviceProvider = this.selectedServiceProvider;
    this.addEditServiceProviderComponent.showSidebar();
    this.selectedServiceProvider = {} as ServiceProvider;
  }
  onCloseSidebar() {
    this.addEditServiceProviderComponent.serviceProvider = this.newServiceProvider;
    this.addEditServiceProviderComponent.hideSideBar();
    this.selectedServiceProvider = {} as ServiceProvider;
    this.isItemSelected = false;
  }

  onDelete() {
    this.showDeleteConfirmModal = true;
  }
  confirmDelete() {
    this.loaderSerivce.showLoader();
    this.selectedServiceProvider.isActive = false;
    this.insertOrUpdateRoleAsync(this.commonPropsService.prepareModelForSave(this.selectedServiceProvider), false, false, false, true);

  }
  cancelDelete() {
    this.showDeleteConfirmModal = false;
  }
  onCopy() {
    this.showCopyConfirmModal = true;
  }
  confirmCopy() {
    this.loaderSerivce.showLoader();
    this.selectedServiceProvider.providerId = 0;
    this.insertOrUpdateRoleAsync(this.commonPropsService.prepareModelForSave(this.selectedServiceProvider), false, false, true, false);
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
    this.serviceProviders.forEach(row => row.isChecked = isChecked);
    if (isChecked) {
      this.selectedItems = this.serviceProviders;
    } else {
      this.selectedItems = [];
    }
  }

  onRowCheckboxChange(row: any, event: any) {
    const allChecked = this.serviceProviders.every(row => row.isChecked);
    const anyUnchecked = this.serviceProviders.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
    if (row.isChecked) {
      this.selectedServiceProvider = row;
      this.isItemSelected = true;
      const multipleServiceProviders: ServiceProvider[] = [];
      multipleServiceProviders.push(row);
      this.addOrUpdateServiceProviders(multipleServiceProviders);
    } else {
      this.isItemSelected = false;
      this.selectedServiceProvider = {} as ServiceProvider;
      this.selectedItems = this.selectedItems.filter(item => item.providerId !== row.providerId);
    }
  }

  isAllChecked() {
    return this.serviceProviders.every(row => row.isChecked);
  }
  addOrUpdateServiceProviders(items: ServiceProvider[]): void {
    items.forEach(item => {
      const index = this.selectedItems.findIndex(existingItem => existingItem.providerId === item.providerId);
      if (index !== -1) {
        this.selectedItems[index] = { ...this.selectedItems[index], ...item };
      } else {
        this.selectedItems.push(item);
      }
    });
  }
  handleServiceProviderChange(serviceProvider: ServiceProvider) {
    console.log(serviceProvider);
    this.loaderSerivce.showLoader();
    const isAdd = serviceProvider.providerId === 0;
    const isUpdate = serviceProvider.providerId > 0;
    serviceProvider.isActive = true;
    this.insertOrUpdateRoleAsync(this.commonPropsService.prepareModelForSave(serviceProvider), isAdd, isUpdate, false, false);

  }
  insertOrUpdateRoleAsync(serviceProvider: ServiceProvider, isAdd: boolean, isUpdate: boolean, isCopy: boolean, isDelete: boolean) {
    this.serviceProviderService.insertOrUpdateerviceProviderAsync(serviceProvider).subscribe(response => {
      if (response) {
        if (isAdd)
          this.tosterService.success("role added sucessful");
        else if (isUpdate)
          this.tosterService.success("role updated sucessful");
        else if (isCopy) {
          this.isItemSelected = false;
          this.selectedServiceProvider = {} as ServiceProvider;
          this.showCopyConfirmModal = false;
          this.tosterService.success("role copied sucessful");
        }
        else if (isDelete) {
          this.isItemSelected = false;
          this.selectedServiceProvider = {} as ServiceProvider;
          this.showDeleteConfirmModal = false;
          this.tosterService.success("role deleted sucessful");
        }
      }
      this.fecthServiceProvidersAsync();
      this.loaderSerivce.hideLoader();
    });
  }
}
