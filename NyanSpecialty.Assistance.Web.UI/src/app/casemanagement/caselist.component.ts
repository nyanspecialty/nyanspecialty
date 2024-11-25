import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild, viewChild, ViewEncapsulation } from '@angular/core';
import { CaseService } from '../services/case.service';
import { LoaderService } from '../services/loader.service';
import { ToastrService } from 'ngx-toastr';
import { CommonPropsService } from '../services/common.service';
import { Case } from '../models/case';
import { CaseDetails } from '../models/casedetails';
import { AddEditCaseComponent } from './caseaddedit.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';


@Component({
  selector: 'app-caselist',
  standalone: true,
  imports: [AddEditCaseComponent,
    FormsModule,
    CommonModule,
    CopyconfirmComponent,
    DeleteconfirmComponent,
    GridHeaderComponent,
    NgxDatatableModule
  ],
  templateUrl: './caselist.component.html',
  styleUrl: './caselist.component.css',
  encapsulation: ViewEncapsulation.None,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class CaselistComponent implements OnInit {

  dbInsuranceCases: CaseDetails[] = [];

  mappedInsuranceCases: any[] = [];

  filteredRows: CaseDetails[] = [];

  selectedCaseDetails: CaseDetails = {} as CaseDetails;

  selectedCase: Case = {} as Case;

  newCaseDetails: CaseDetails = {} as CaseDetails;

  newCase: Case = {} as Case;

  @ViewChild(AddEditCaseComponent) addEditCaseComponent!: AddEditCaseComponent;

  isItemSelected: boolean = false;

  showDeleteConfirmModal: boolean = false;

  showCopyConfirmModal: boolean = false;

  headerCheckboxChecked: boolean = false;

  selectedItems: CaseDetails[] = [];

  constructor(private caseService: CaseService,
    private loaderService: LoaderService,
    private tosterService: ToastrService,
    private commonPropsService: CommonPropsService
  ) { }
  ngOnInit() {
    this.loaderService.showLoader();
    this.fetchInsuranceCasesAsync();
  }

  fetchInsuranceCasesAsync() {
    this.caseService.GetCaseDetailsAsync().subscribe(response => {
      this.dbInsuranceCases = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as CaseDetails[];
      this.filteredRows = this.dbInsuranceCases;
      this.loaderService.hideLoader();
    });
  }
  onAdd() {
    console.log('Add button clicked in GridHeader');
    this.addEditCaseComponent.caseDetails = this.newCase;
    this.addEditCaseComponent.showSidebar();
  }

  onEdit() {
    console.log('Edit button clicked in GridHeader');
    this.isItemSelected = false;
    this.addEditCaseComponent.caseDetails = this.selectedCaseDetails.caseDetails;
    this.selectedCase = this.selectedCaseDetails.caseDetails;
    this.addEditCaseComponent.showSidebar();
    this.selectedCase = {} as Case;
    this.selectedCaseDetails = {} as CaseDetails;
  }
  onCloseSidebar() {
    this.addEditCaseComponent.caseDetails = this.newCase;
    this.addEditCaseComponent.hideSideBar();
    this.selectedCaseDetails = {} as CaseDetails;
    this.selectedCase = {} as Case;
    this.isItemSelected = false;
  }

  onDelete() {
    this.showDeleteConfirmModal = true;
  }
  confirmDelete() {
    this.loaderService.showLoader();
    this.selectedCase.isActive = false;
    this.insertOrUpdateCaseAsync(this.commonPropsService.prepareModelForSave(this.selectedCase), false, false, false, true);

  }
  cancelDelete() {
    this.showDeleteConfirmModal = false;
  }
  onCopy() {
    this.showCopyConfirmModal = true;
  }
  confirmCopy() {
    this.loaderService.showLoader();
    this.selectedCase.caseId = 0;
    this.insertOrUpdateCaseAsync(this.commonPropsService.prepareModelForSave(this.selectedCase), false, false, true, false);
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
    this.dbInsuranceCases.forEach(row => row.isChecked = isChecked);
    if (isChecked) {
      this.selectedItems = this.dbInsuranceCases;
    } else {
      this.selectedItems = [];
    }
  }

  onRowCheckboxChange(row: any, event: any) {
    const allChecked = this.dbInsuranceCases.every(row => row.isChecked);
    const anyUnchecked = this.dbInsuranceCases.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
    if (row.isChecked) {
      this.selectedCaseDetails = row;
      this.isItemSelected = true;
      const multipleCustomers: CaseDetails[] = [];
      multipleCustomers.push(row);
      this.addOrUpdateCustomers(multipleCustomers);
    } else {
      this.isItemSelected = false;
      this.selectedCase = {} as Case;
      this.selectedItems = this.selectedItems.filter(item => item.caseDetails.caseId !== row.caseId);
    }
  }

  isAllChecked() {
    return this.dbInsuranceCases.every(row => row.isChecked);
  }
  addOrUpdateCustomers(items: CaseDetails[]): void {
    items.forEach(item => {
      const index = this.selectedItems.findIndex(existingItem => existingItem.caseDetails.caseId === item.caseDetails.caseId);
      if (index !== -1) {
        this.selectedItems[index] = { ...this.selectedItems[index], ...item };
      } else {
        this.selectedItems.push(item);
      }
    });
  }
  handleCaseChange(caseDetails: Case) {
    console.log(caseDetails);
    this.loaderService.showLoader();
    const isAdd = caseDetails.caseId === 0;
    const isUpdate = caseDetails.caseId > 0;
    caseDetails.isActive = true;
    this.insertOrUpdateCaseAsync(this.commonPropsService.prepareModelForSave(caseDetails), isAdd, isUpdate, false, false);

  }
  insertOrUpdateCaseAsync(caseDetals: Case, isAdd: boolean, isUpdate: boolean, isCopy: boolean, isDelete: boolean) {
    this.caseService.InsertOrUpdateCaseAsync(caseDetals).subscribe(response => {
      if (response) {
        if (isAdd)
          this.tosterService.success("role added sucessful");
        else if (isUpdate)
          this.tosterService.success("role updated sucessful");
        else if (isCopy) {
          this.isItemSelected = false;
          this.selectedCase = {} as Case;
          this.selectedCaseDetails = {} as CaseDetails;
          this.showCopyConfirmModal = false;
          this.tosterService.success("role copied sucessful");
        }
        else if (isDelete) {
          this.isItemSelected = false;
          this.selectedCase = {} as Case;
          this.selectedCaseDetails = {} as CaseDetails;
          this.showDeleteConfirmModal = false;
          this.tosterService.success("role deleted sucessful");
        }
      }
      this.fetchInsuranceCasesAsync();
      this.loaderService.hideLoader();
    });
  }
}
