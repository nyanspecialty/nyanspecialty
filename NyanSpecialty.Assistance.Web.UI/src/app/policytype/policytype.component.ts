import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { PolicyTypeService } from '../services/policytype.service';
import { PolicyType } from '../models/policytype';
import { LoaderService } from '../services/loader.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';
import { AddEditPolicyTypeComponent } from './addeditpolicytype.component';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';

@Component({
  selector: 'app-policytype',
  standalone: true,
  imports: [CommonModule,
    NgxDatatableModule,
    FormsModule,
    GridHeaderComponent,
    AddEditPolicyTypeComponent,
    DeleteconfirmComponent,
    CopyconfirmComponent],
  templateUrl: './policytype.component.html',
  styleUrl: './policytype.component.css',
  encapsulation: ViewEncapsulation.None,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class PolicytypeComponent implements OnInit {

  rows: PolicyType[] = [];

  filteredRows: PolicyType[] = [];

  filters: any = {};

  headerCheckboxChecked: boolean = false;

  newPolicyType: PolicyType = {} as PolicyType;

  @ViewChild(AddEditPolicyTypeComponent) addEditPolicyTypeComponent!: AddEditPolicyTypeComponent;

  selectedPolicyType: PolicyType = {} as PolicyType;

  selectedItems: any[] = [];

  isItemSelected: boolean = false;

  showDeleteConfirmModal: boolean = false;

  showCopyConfirmModal: boolean = false;

  constructor(private policyTypeService: PolicyTypeService, private loader: LoaderService) { }

  ngOnInit() {
    this.fetchPolicyTypesAsync();
  }

  public fetchPolicyTypesAsync() {
    this.policyTypeService.getAllPolicyTypeAsync().subscribe(response => {
      this.rows = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as PolicyType[];
      this.filteredRows = this.rows;
    });
    this.loader.hideLoader();

  }
  toggleAll(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.rows.forEach(row => row.isChecked = isChecked);
    if (isChecked) {
      this.selectedItems = this.rows;
    } else {
      this.selectedItems = [];
    }
  }

  onRowCheckboxChange(row: any, event: any) {
    const allChecked = this.rows.every(row => row.isChecked);
    const anyUnchecked = this.rows.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
    if (row.isChecked) {
      this.selectedPolicyType = row;
      this.isItemSelected = true;
      const multiplePolicyTypes: PolicyType[] = [];
      multiplePolicyTypes.push(row);
      this.addOrUpdatePolicyTypes(multiplePolicyTypes);
    } else {
      this.isItemSelected = false;
      this.selectedPolicyType = {} as PolicyType;
      this.selectedItems = this.selectedItems.filter(item => item.policyTypeId !== row.policyTypeId);
    }
  }

  isAllChecked() {
    return this.rows.every(row => row.isChecked);
  }
  onAdd() {
    console.log('Add button clicked in GridHeader');
    this.addEditPolicyTypeComponent.policyType = this.newPolicyType;
    this.addEditPolicyTypeComponent.showSidebar();
  }

  onEdit() {
    console.log('Edit button clicked in GridHeader');
    this.isItemSelected = false;
    this.addEditPolicyTypeComponent.policyType = this.selectedPolicyType;
    this.addEditPolicyTypeComponent.showSidebar();
    this.selectedPolicyType = {} as PolicyType;
    // Implement your logic here
  }

  onDelete() {
    this.showDeleteConfirmModal = true;
  }
  confirmDelete() {
    this.loader.showLoader();
    console.log('Confirmed delete for:', this.selectedPolicyType);
    this.selectedPolicyType.isActive = false;
    this.policyTypeService.insertOrUpdatePolicyTypeAsync(this.selectedPolicyType).subscribe(response => {
      this.fetchPolicyTypesAsync();
      this.isItemSelected = false;
      this.selectedPolicyType = {} as PolicyType;
      this.loader.hideLoader();
      this.showDeleteConfirmModal = false;
    });
  }

  cancelDelete() {
    this.showDeleteConfirmModal = false;
  }
  onCopy() {
    this.showCopyConfirmModal = true;
  }
  confirmCopy() {
    this.loader.showLoader();
    console.log('Copy button clicked in GridHeader');
    // Implement your logic here
    this.selectedPolicyType.policyTypeId = 0;
    this.policyTypeService.insertOrUpdatePolicyTypeAsync(this.selectedPolicyType).subscribe(response => {
      this.isItemSelected = false;
      this.selectedPolicyType = {} as PolicyType;
      this.fetchPolicyTypesAsync();
      this.showCopyConfirmModal = false;
      this.loader.hideLoader();
    });
  }
  cancelCopy() {
    this.showCopyConfirmModal = false;
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
  handlePolicyTypeChange(policyType: PolicyType) {
    this.loader.showLoader();
    console.log('Received policy type from child:', policyType);
    policyType.createdBy = 1;
    policyType.createdOn = new Date();
    policyType.modifiedBy = 1;
    policyType.modifiedOn = new Date();
    policyType.isActive = true;

    this.policyTypeService.insertOrUpdatePolicyTypeAsync(policyType).subscribe(response => {
      this.fetchPolicyTypesAsync();
    });
  }

  addOrUpdatePolicyTypes(items: PolicyType[]): void {
    items.forEach(item => {
      const index = this.selectedItems.findIndex(existingItem => existingItem.policyTypeId === item.policyTypeId);
      if (index !== -1) {
        this.selectedItems[index] = { ...this.selectedItems[index], ...item };
      } else {
        this.selectedItems.push(item);
      }
    });
  }
}