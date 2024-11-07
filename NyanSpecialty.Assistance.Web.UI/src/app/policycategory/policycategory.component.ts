import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { LoaderService } from '../services/loader.service';
import { PolicyCategoryService } from '../services/policycategory.service';
import { ToastrService } from 'ngx-toastr';
import { PolicyCategory } from '../models/policycategory';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { AddEditPolicyCategoryComponent } from './add-edit-policycategory.component';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';
import { CommonPropsService } from '../services/common.service';

@Component({
  selector: 'app-policycategory',
  standalone: true,
  imports: [FormsModule, CommonModule, NgxDatatableModule,
    AddEditPolicyCategoryComponent,
    GridHeaderComponent,
    DeleteconfirmComponent,
    CopyconfirmComponent
  ],
  templateUrl: './policycategory.component.html',
  styleUrl: './policycategory.component.css',
  encapsulation: ViewEncapsulation.None,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class PolicyCategoryComponent implements OnInit {

  policyCategories: PolicyCategory[] = [];

  filteredRows: PolicyCategory[] = [];

  selectedItems: PolicyCategory[] = [];

  headerCheckboxChecked: boolean = false;

  selectedPolicyCategory: PolicyCategory = {} as PolicyCategory;

  newPolicyCategory: PolicyCategory = {} as PolicyCategory;

  isItemSelected: boolean = false;

  @ViewChild(AddEditPolicyCategoryComponent) addEditPolicyCategoryComponent!: AddEditPolicyCategoryComponent;

  showDeleteConfirmModal: boolean = false;

  showCopyConfirmModal: boolean = false;

  constructor(private loaderService: LoaderService,
    private policyCategoryService: PolicyCategoryService,
    private tosterService: ToastrService, private commonPropsService: CommonPropsService) {

  }
  ngOnInit() {
    this.loaderService.showLoader();
    this.fetchPolicyCategories();
  }
  fetchPolicyCategories() {
    this.policyCategoryService.fetchPolicyCategoriesAsync().subscribe(response => {
      this.policyCategories = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as PolicyCategory[];
      this.filteredRows = this.policyCategories;
      this.loaderService.hideLoader();
    });
  }
  toggleAll(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.policyCategories.forEach(row => row.isChecked = isChecked);
    if (isChecked) {
      this.selectedItems = this.policyCategories;
    } else {
      this.selectedItems = [];
    }
  }

  onRowCheckboxChange(row: any, event: any) {
    const allChecked = this.policyCategories.every(row => row.isChecked);
    const anyUnchecked = this.policyCategories.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
    if (row.isChecked) {
      this.selectedPolicyCategory = row;
      this.isItemSelected = true;
      const multiplePolicyTypes: PolicyCategory[] = [];
      multiplePolicyTypes.push(row);
      this.addOrUpdatePolicyCategorys(multiplePolicyTypes);
    } else {
      this.isItemSelected = false;
      this.selectedPolicyCategory = {} as PolicyCategory;
      this.selectedItems = this.selectedItems.filter(item => item.policyCategoryId !== row.policyCategoryId);
    }
  }

  isAllChecked() {
    return this.policyCategories.every(row => row.isChecked);
  }
  addOrUpdatePolicyCategorys(items: PolicyCategory[]): void {
    items.forEach(item => {
      const index = this.selectedItems.findIndex(existingItem => existingItem.policyCategoryId === item.policyCategoryId);
      if (index !== -1) {
        this.selectedItems[index] = { ...this.selectedItems[index], ...item };
      } else {
        this.selectedItems.push(item);
      }
    });
  }
  onAdd() {
    console.log('Add button clicked in GridHeader');
    this.addEditPolicyCategoryComponent.policyCategory = this.newPolicyCategory;
    this.addEditPolicyCategoryComponent.showSidebar();
  }
  handlePolicyCategoryChange(policyCategory: PolicyCategory) {
    this.loaderService.showLoader();
    console.log('Received policy category from child:', policyCategory);
    const isAdd = policyCategory.policyCategoryId === 0;
    const isUpdate = policyCategory.policyCategoryId > 0;
    policyCategory.isActive = true;
    this.handleInsertOrUpdate(this.commonPropsService.prepareModelForSave(policyCategory), false, isUpdate, isAdd, false);
  }
  onEdit() {
    console.log('Edit button clicked in GridHeader');
    this.isItemSelected = false;
    this.addEditPolicyCategoryComponent.policyCategory = this.selectedPolicyCategory;
    this.addEditPolicyCategoryComponent.showSidebar();
    this.selectedPolicyCategory = {} as PolicyCategory;
    // Implement your logic here
  }
  onCloseSidebar() {
    this.addEditPolicyCategoryComponent.policyCategory = this.newPolicyCategory;
    this.addEditPolicyCategoryComponent.hideSideBar();
    this.selectedPolicyCategory = {} as PolicyCategory;
    this.isItemSelected = false;
  }
  onDelete() {
    this.showDeleteConfirmModal = true;
  }
  confirmDelete() {
    this.loaderService.showLoader();
    console.log('Confirmed delete for:', this.selectedPolicyCategory);
    this.selectedPolicyCategory.isActive = false;
    this.handleInsertOrUpdate(this.commonPropsService.prepareModelForSave(this.selectedPolicyCategory), false, false, false, true);
  }

  cancelDelete() {
    this.showDeleteConfirmModal = false;
    this.isItemSelected = false;
  }
  onCopy() {
    this.showCopyConfirmModal = true;
  }
  confirmCopy() {
    this.loaderService.showLoader();
    console.log('Copy button clicked in GridHeader');
    this.selectedPolicyCategory.policyCategoryId = 0;
    this.handleInsertOrUpdate(this.commonPropsService.prepareModelForSave(this.selectedPolicyCategory), true, false, false, false);
  }
  cancelCopy() {
    this.showCopyConfirmModal = false;
    this.isItemSelected = false;
  }
  onImport() {
    console.log('Import button clicked in GridHeader');
    // Implement your logic here
  }

  onExportTemplate() {
    console.log('Export Template button clicked in GridHeader');
    // Implement your logic here
  }

  onExportWithGridData() {
    console.log('Export with Grid Data button clicked in GridHeader');
    // Implement your logic here
  }

  onExportWithOriginalData() {
    console.log('Export with Original Data button clicked in GridHeader');
    // Implement your logic here
  }
  handleInsertOrUpdate(policyCategory: PolicyCategory, isCopy: boolean, isUpdate: boolean, isAdd: boolean, isDelete: boolean) {
    this.policyCategoryService.insertOrUpdatePolicyCategoryAsync(policyCategory).subscribe(response => {
      if (response) {
        if (isAdd)
          this.tosterService.success("policy category added sucessful");
        else if (isUpdate)
          this.tosterService.success("policy category updated sucessful");
        else if (isCopy) {
          this.isItemSelected = false;
          this.selectedPolicyCategory = {} as PolicyCategory;
          this.showCopyConfirmModal = false;
          this.tosterService.success("policy category copied sucessful");
        }
        else if (isDelete) {
          this.isItemSelected = false;
          this.selectedPolicyCategory = {} as PolicyCategory;
          this.showDeleteConfirmModal = false;
          this.tosterService.success("policy category deleted sucessful");
        }
      }
      this.fetchPolicyCategories();
      this.loaderService.hideLoader();
    });
  }
}
