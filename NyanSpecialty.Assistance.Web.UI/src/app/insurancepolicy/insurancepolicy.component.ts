import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { InsurancePolicyService } from '../services/insurancepolicy.service';
import { LoaderService } from '../services/loader.service';
import { ToastrService } from 'ngx-toastr';
import { InsurancePolicy } from '../models/insurancepolicy';
import { AddEditPolicyComponent } from './add-edit-policy.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { CommonPropsService } from '../services/common.service';

@Component({
  selector: 'app-insurancepolicy',
  standalone: true,
  imports: [CommonModule,
    FormsModule,
    AddEditPolicyComponent,
    GridHeaderComponent,
    CopyconfirmComponent,
    DeleteconfirmComponent,
    NgxDatatableModule],
  templateUrl: './insurancepolicy.component.html',
  styleUrl: './insurancepolicy.component.css',
  encapsulation: ViewEncapsulation.None,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class InsurancePolicyComponent implements OnInit {

  insurancePolicies: InsurancePolicy[] = [];

  selectedItems: InsurancePolicy[] = [];

  filteredRows: InsurancePolicy[] = [];

  isItemSelected: boolean = false;

  showDeleteConfirmModal: boolean = false;

  showCopyConfirmModal: boolean = false;

  headerCheckboxChecked: boolean = false;

  selectedInsurancePolicy: InsurancePolicy = {} as InsurancePolicy;

  newInsurancePolicy: InsurancePolicy = {} as InsurancePolicy;

  @ViewChild(AddEditPolicyComponent) addEditPolicyComponent!: AddEditPolicyComponent;

  pageSize: number = 100; 

  currentPage: number = 1; 

  totalItems: number = 0; 

  constructor(private insurancePolicyService: InsurancePolicyService,
    private loaderService: LoaderService,
    private tosterService: ToastrService,
    private commonPropsService: CommonPropsService) { }

  ngOnInit(): void {
    this.loaderService.showLoader();
    this.fetchInsurancePoliciesAsync();
  }

  fetchInsurancePoliciesAsync() {
    this.insurancePolicyService.fetchInsurancePolicysAsync().subscribe(response => {
      this.insurancePolicies = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as InsurancePolicy[];
      this.filteredRows = this.insurancePolicies.slice(0, this.pageSize);
      this.totalItems = this.filteredRows.length;
    });
  }
  onAdd() {
    console.log('Add button clicked in GridHeader');
    this.addEditPolicyComponent.insurancePolicy = this.newInsurancePolicy;
    this.addEditPolicyComponent.showSidebar();
  }

  onEdit() {
    console.log('Edit button clicked in GridHeader');
    this.isItemSelected = false;
    this.addEditPolicyComponent.insurancePolicy = this.selectedInsurancePolicy;
    this.addEditPolicyComponent.showSidebar();
    this.selectedInsurancePolicy = {} as InsurancePolicy;
  }
  onCloseSidebar() {
    this.addEditPolicyComponent.insurancePolicy = this.newInsurancePolicy;
    this.addEditPolicyComponent.hideSideBar();
    this.selectedInsurancePolicy = {} as InsurancePolicy;
    this.isItemSelected = false;
  }

  onDelete() {
    this.showDeleteConfirmModal = true;
  }
  confirmDelete() {
    this.loaderService.showLoader();
    this.selectedInsurancePolicy.isActive = false;
    this.insertOrUpdateInsurancePolicyAsync(this.commonPropsService.prepareModelForSave(this.selectedInsurancePolicy), false, false, false, true);

  }
  cancelDelete() {
    this.showDeleteConfirmModal = false;
  }
  onCopy() {
    this.showCopyConfirmModal = true;
  }
  confirmCopy() {
    this.loaderService.showLoader();
    this.selectedInsurancePolicy.insurancePolicyId = 0;
    this.insertOrUpdateInsurancePolicyAsync(this.commonPropsService.prepareModelForSave(this.selectedInsurancePolicy), false, false, true, false);
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
    this.insurancePolicies.forEach(row => row.isChecked = isChecked);
    if (isChecked) {
      this.selectedItems = this.insurancePolicies;
    } else {
      this.selectedItems = [];
    }
  }

  onRowCheckboxChange(row: any, event: any) {
    const allChecked = this.insurancePolicies.every(row => row.isChecked);
    const anyUnchecked = this.insurancePolicies.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
    if (row.isChecked) {
      this.selectedInsurancePolicy = row;
      this.isItemSelected = true;
      const multipleInsurancePolicys: InsurancePolicy[] = [];
      multipleInsurancePolicys.push(row);
      this.addOrUpdateInsurancePolicys(multipleInsurancePolicys);
    } else {
      this.isItemSelected = false;
      this.selectedInsurancePolicy = {} as InsurancePolicy;
      this.selectedItems = this.selectedItems.filter(item => item.insurancePolicyId !== row.insurancePolicyId);
    }
  }

  isAllChecked() {
    return this.insurancePolicies.every(row => row.isChecked);
  }
  addOrUpdateInsurancePolicys(items: InsurancePolicy[]): void {
    items.forEach(item => {
      const index = this.selectedItems.findIndex(existingItem => existingItem.insurancePolicyId === item.insurancePolicyId);
      if (index !== -1) {
        this.selectedItems[index] = { ...this.selectedItems[index], ...item };
      } else {
        this.selectedItems.push(item);
      }
    });
  }
  handleInsurancePolicyChange(insurancePolicy: InsurancePolicy) {
    console.log(insurancePolicy);
    this.loaderService.showLoader();
    const isAdd = insurancePolicy.insurancePolicyId === 0;
    const isUpdate = insurancePolicy.insurancePolicyId > 0;
    insurancePolicy.isActive = true;
    this.insertOrUpdateInsurancePolicyAsync(this.commonPropsService.prepareModelForSave(insurancePolicy), isAdd, isUpdate, false, false);

  }
  insertOrUpdateInsurancePolicyAsync(insurancePolicy: InsurancePolicy, isAdd: boolean, isUpdate: boolean, isCopy: boolean, isDelete: boolean) {
    this.insurancePolicyService.insertOrUpdateInsurancePolicyAsync(insurancePolicy).subscribe(response => {
      if (response) {
        if (isAdd)
          this.tosterService.success("policy insurance policy added sucessful");
        else if (isUpdate)
          this.tosterService.success("policy insurance policy updated sucessful");
        else if (isCopy) {
          this.isItemSelected = false;
          this.selectedInsurancePolicy = {} as InsurancePolicy;
          this.showCopyConfirmModal = false;
          this.tosterService.success("policy workflow copied sucessful");
        }
        else if (isDelete) {
          this.isItemSelected = false;
          this.selectedInsurancePolicy = {} as InsurancePolicy;
          this.showDeleteConfirmModal = false;
          this.tosterService.success("policy workflow deleted sucessful");
        }
      }
      this.fetchInsurancePoliciesAsync();
      this.loaderService.hideLoader();
    });
  }
  onScroll(event: any): void {
    const scrollPosition = event.offsetY;
    const tableViewHeight = event.viewHeight;
    const tableContentHeight = event.scrollHeight;
  
    // Check if we are near the bottom of the scroll
    if (scrollPosition + tableViewHeight >= tableContentHeight - 50) {
      this.loadMoreRows();
    }
  }
  
  loadMoreRows(): void {
    const nextRows = this.insurancePolicies.slice(
      this.filteredRows.length,
      this.filteredRows.length + this.pageSize
    );
    
    if (nextRows.length) {
      this.filteredRows = [...this.filteredRows, ...nextRows];
    }
  }
}
