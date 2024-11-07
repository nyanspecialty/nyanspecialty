import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';
import { AddEditVehicleClassComponent } from './add-edit-vehicleclass.component';
import { LoaderService } from '../services/loader.service';
import { ToastrService } from 'ngx-toastr';
import { CommonPropsService } from '../services/common.service';
import { VehicleClassService } from '../services/vehicleclass.service';
import { VehicleClass } from '../models/vehicleclass';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';

@Component({
  selector: 'app-vehicleclass',
  standalone: true,
  imports: [CommonModule,
    FormsModule,
    NgxDatatableModule,
    GridHeaderComponent,
    CopyconfirmComponent,
    DeleteconfirmComponent,
    AddEditVehicleClassComponent
  ],
  templateUrl: './vehicleclass.component.html',
  styleUrl: './vehicleclass.component.css',
  encapsulation: ViewEncapsulation.None,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class VehicleClassComponent implements OnInit {

  vehicleClasses: VehicleClass[] = [];

  selectedItems: VehicleClass[] = [];

  filteredRows: VehicleClass[] = [];

  @ViewChild(AddEditVehicleClassComponent) addEditVehicleClassComponent!: AddEditVehicleClassComponent;

  isItemSelected: boolean = false;

  showDeleteConfirmModal: boolean = false;

  showCopyConfirmModal: boolean = false;

  headerCheckboxChecked: boolean = false;

  selectedVehicleClass: VehicleClass = {} as VehicleClass;

  newVehicleClass: VehicleClass = {} as VehicleClass;

  constructor(private loaderService: LoaderService,
    private tosterService: ToastrService,
    private commonPropsService: CommonPropsService,
    private vehiccleClassServiec: VehicleClassService) { }

  ngOnInit() {
    this.loaderService.showLoader();
    this.fetchVehicleClassesAsync();
  }
  fetchVehicleClassesAsync() {
    this.vehiccleClassServiec.fetchVehicleClassesAsync().subscribe(response => {
      this.vehicleClasses = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as VehicleClass[];
      this.filteredRows = this.vehicleClasses;
      this.loaderService.hideLoader();
    });
  }
  handleVehicleClassChange(vehicleClass: VehicleClass) {
    console.log(vehicleClass);
    this.loaderService.showLoader();
    const isAdd = (!vehicleClass.vehicleClassId || vehicleClass.vehicleClassId == 0) ? true : false;
    const isUpdate = vehicleClass.vehicleClassId > 0;
    vehicleClass.isActive = true;
    this.insertOrUpdateVehiclesizeAsync(this.commonPropsService.prepareModelForSave(vehicleClass), isAdd, isUpdate, false, false);
  }
  onCloseSidebar() {
    this.addEditVehicleClassComponent.vehicleClass = this.newVehicleClass;
    this.addEditVehicleClassComponent.hideSideBar();
    this.selectedVehicleClass = {} as VehicleClass;
    this.isItemSelected = false;
  }
  onAdd() {
    console.log('Add button clicked in GridHeader');
    this.addEditVehicleClassComponent.vehicleClass = this.newVehicleClass;
    this.addEditVehicleClassComponent.showSidebar();
  }
  onEdit() {
    console.log('Edit button clicked in GridHeader');
    this.isItemSelected = false;
    this.addEditVehicleClassComponent.vehicleClass = this.selectedVehicleClass;
    this.addEditVehicleClassComponent.showSidebar();
    this.selectedVehicleClass = {} as VehicleClass;
  }
  onDelete() {
    this.showDeleteConfirmModal = true;
  }
  confirmDelete() {
    this.loaderService.showLoader();
    this.selectedVehicleClass.isActive = false;
    this.insertOrUpdateVehiclesizeAsync(this.commonPropsService.prepareModelForSave(this.selectedVehicleClass), false, false, false, true);
  }
  cancelDelete() {
    this.showDeleteConfirmModal = false;
  }
  onCopy() {
    this.showCopyConfirmModal = true;
  }
  confirmCopy() {
    this.loaderService.showLoader();
    this.selectedVehicleClass.vehicleClassId = 0;
    this.insertOrUpdateVehiclesizeAsync(this.commonPropsService.prepareModelForSave(this.selectedVehicleClass), false, false, true, false);
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
    this.vehicleClasses.forEach(row => row.isChecked = isChecked);
    if (isChecked) {
      this.selectedItems = this.vehicleClasses;
    } else {
      this.selectedItems = [];
    }
  }

  onRowCheckboxChange(row: any, event: any) {
    const allChecked = this.vehicleClasses.every(row => row.isChecked);
    const anyUnchecked = this.vehicleClasses.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
    if (row.isChecked) {
      this.selectedVehicleClass = row;
      this.isItemSelected = true;
      const multipleVehiclesizes: VehicleClass[] = [];
      multipleVehiclesizes.push(row);
      this.addOrUpdateVehiclesizes(multipleVehiclesizes);
    } else {
      this.isItemSelected = false;
      this.selectedVehicleClass = {} as VehicleClass;
      this.selectedItems = this.selectedItems.filter(item => item.vehicleClassId !== row.vehicleClassId);
    }
  }

  isAllChecked() {
    return this.vehicleClasses.every(row => row.isChecked);
  }
  addOrUpdateVehiclesizes(items: VehicleClass[]): void {
    items.forEach(item => {
      const index = this.selectedItems.findIndex(existingItem => existingItem.vehicleClassId === item.vehicleClassId);
      if (index !== -1) {
        this.selectedItems[index] = { ...this.selectedItems[index], ...item };
      } else {
        this.selectedItems.push(item);
      }
    });
  }
  insertOrUpdateVehiclesizeAsync(vehicleClass: VehicleClass, isAdd: boolean, isUpdate: boolean, isCopy: boolean, isDelete: boolean) {
    this.vehiccleClassServiec.insertOrUpdateVehicleClassAsync(vehicleClass).subscribe(response => {
      if (response) {
        if (isAdd)
          this.tosterService.success("vehicle class added sucessful");
        else if (isUpdate)
          this.tosterService.success("vehicle class updated sucessful");
        else if (isCopy) {
          this.isItemSelected = false;
          this.selectedVehicleClass = {} as VehicleClass;
          this.showCopyConfirmModal = false;
          this.tosterService.success("vehicle class copied sucessful");
        }
        else if (isDelete) {
          this.isItemSelected = false;
          this.selectedVehicleClass = {} as VehicleClass;
          this.showDeleteConfirmModal = false;
          this.tosterService.success("vehicle class deleted sucessful");
        }
      }
      this.fetchVehicleClassesAsync();
      this.loaderService.hideLoader();
    });
  }
}
