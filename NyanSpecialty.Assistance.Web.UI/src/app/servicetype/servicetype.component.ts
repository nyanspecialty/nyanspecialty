import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ServiceTypeService } from '../services/servicetype.service';
import { ServiceType } from '../models/servicetype';
import { CommonModule } from '@angular/common';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';
import { FormsModule } from '@angular/forms';
import { AddEditServiceTypeComponent } from './add-edit-servicetype.component';
import { LoaderService } from '../services/loader.service';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';

@Component({
  selector: 'app-servicetype',
  standalone: true,
  imports: [CommonModule,
    NgxDatatableModule,
    FormsModule,
    GridHeaderComponent,
    AddEditServiceTypeComponent,
    DeleteconfirmComponent,
    CopyconfirmComponent],
  templateUrl: './servicetype.component.html',
  styleUrl: './servicetype.component.css',
  encapsulation: ViewEncapsulation.None,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ServicetypeComponent implements OnInit {

  serviceTypes: ServiceType[] = [];

  selectedItems: ServiceType[] = [];

  filteredRows: ServiceType[] = [];

  isItemSelected: boolean = false;

  @ViewChild(AddEditServiceTypeComponent) addEditServiceTypeComponent!: AddEditServiceTypeComponent;

  newServiceType: ServiceType = {} as ServiceType;

  selectedServiceType: ServiceType = {} as ServiceType;

  headerCheckboxChecked: boolean = false;

  showDeleteConfirmModal: boolean = false;

  showCopyConfirmModal: boolean = false;

  constructor(private serviceTypeService: ServiceTypeService, private loader: LoaderService) { }
  ngOnInit() {
    this.fetchServiceTypesAsync();
  }
  fetchServiceTypesAsync() {
    this.serviceTypeService.fetchServiceTypesAsync().subscribe(response => {
      this.serviceTypes = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as ServiceType[];
      this.filteredRows = this.serviceTypes;
    });
  }
  onAdd() {
    console.log('Add button clicked in GridHeader');
    this.addEditServiceTypeComponent.serviceType = this.newServiceType;
    this.addEditServiceTypeComponent.showSideBar();
  }
  onEdit() {
    console.log('Edit button clicked in GridHeader');
    this.isItemSelected = false;
    this.addEditServiceTypeComponent.serviceType = this.selectedServiceType;
    this.addEditServiceTypeComponent.showSideBar();
    this.selectedServiceType = {} as ServiceType;
  }
  onDelete() {
    this.showDeleteConfirmModal = true;
  }
  confirmDelete() {
    this.loader.showLoader();
    console.log('Confirmed delete for:', this.selectedServiceType);
    this.selectedServiceType.isActive = false;
    this.serviceTypeService.insertOrUpdateServiceTypeAsync(this.selectedServiceType).subscribe(response => {
      this.fetchServiceTypesAsync();
      this.isItemSelected = false;
      this.selectedServiceType = {} as ServiceType;
      this.loader.hideLoader();
      this.showDeleteConfirmModal = false;
    });
  }
  cancelDelete(){
    this.showDeleteConfirmModal = false;
  }
  onCopy() {
    this.showCopyConfirmModal = true;
  }
  confirmCopy(){
    this.loader.showLoader();
    console.log('Confirmed copy for:', this.selectedServiceType);
    this.selectedServiceType.serviceTypeId = 0;
    this.serviceTypeService.insertOrUpdateServiceTypeAsync(this.selectedServiceType).subscribe(response => {
      this.fetchServiceTypesAsync();
      this.isItemSelected = false;
      this.selectedServiceType = {} as ServiceType;
      this.loader.hideLoader();
      this.showCopyConfirmModal = false;
    });
  }
  cancelCopy()
  {
    this.showCopyConfirmModal = false;
  }
  onExportTemplate() {

  }
  onImport() {

  }
  onExportWithGridData() {

  }
  onExportWithOriginalData() {

  }
  handleServiceTypeChange(serviceType: ServiceType) {
    this.loader.showLoader();
    console.log('Received service type from child:', serviceType);
    serviceType.createdBy = 1;
    serviceType.createdOn = new Date();
    serviceType.modifiedBy = 1;
    serviceType.modifiedOn = new Date();
    serviceType.isActive = true;

    this.serviceTypeService.insertOrUpdateServiceTypeAsync(serviceType).subscribe(response => {
      this.fetchServiceTypesAsync();
      this.loader.hideLoader();
    });
  }


  toggleAll(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.serviceTypes.forEach(row => row.isChecked = isChecked);
    if (isChecked) {
      this.selectedItems = this.serviceTypes;
    } else {
      this.selectedItems = [];
    }
  }

  onRowCheckboxChange(row: any, event: any) {
    const allChecked = this.serviceTypes.every(row => row.isChecked);
    const anyUnchecked = this.serviceTypes.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
    if (row.isChecked) {
      this.selectedServiceType = row;
      this.isItemSelected = true;
      const multipleServiceTypes: ServiceType[] = [];
      multipleServiceTypes.push(row);
      this.addOrUpdateServiceTypes(multipleServiceTypes);
    } else {
      this.isItemSelected = false;
      this.selectedServiceType = {} as ServiceType;
      this.selectedItems = this.selectedItems.filter(item => item.serviceTypeId !== row.serviceTypeId);
    }
  }

  isAllChecked() {
    return this.serviceTypes.every(row => row.isChecked);
  }
  addOrUpdateServiceTypes(items: ServiceType[]): void {
    items.forEach(item => {
      const index = this.selectedItems.findIndex(existingItem => existingItem.serviceTypeId === item.serviceTypeId);
      if (index !== -1) {
        this.selectedItems[index] = { ...this.selectedItems[index], ...item };
      } else {
        this.selectedItems.push(item);
      }
    });
  }
}
