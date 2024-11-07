import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { LoaderService } from '../services/loader.service';
import { ToastrService } from 'ngx-toastr';
import { CommonPropsService } from '../services/common.service';
import { VehiclesizeService } from '../services/vehiclesize.service';
import { Vehiclesize } from '../models/vehiclesize';
import { AddEditVehiclesizeComponent } from './add-edit-vehiclesize.component';
import { GridHeaderComponent } from '../shared/grid-header/grid-header.component';
import { CopyconfirmComponent } from '../shared/copyconfirm/copyconfirm.component';
import { DeleteconfirmComponent } from '../shared/deleteconfirm/deleteconfirm.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@Component({
  selector: 'app-vehiclesize',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NgxDatatableModule,
    AddEditVehiclesizeComponent,
    GridHeaderComponent,
    CopyconfirmComponent,
    DeleteconfirmComponent
  ],
  templateUrl: './vehiclesize.component.html',
  styleUrl: './vehiclesize.component.css',
  encapsulation: ViewEncapsulation.None,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class VehiclesizeComponent implements OnInit {

  vehicleSizes: Vehiclesize[] = [];

  selectedItems: Vehiclesize[] = [];

  filteredRows: Vehiclesize[] = [];

  @ViewChild(AddEditVehiclesizeComponent) addEditVehiclesizeComponent!: AddEditVehiclesizeComponent;

  isItemSelected: boolean = false;

  showDeleteConfirmModal: boolean = false;

  showCopyConfirmModal: boolean = false;

  headerCheckboxChecked: boolean = false;

  selectedVehiclesize: Vehiclesize = {} as Vehiclesize;

  newVehiclesize: Vehiclesize = {} as Vehiclesize;

  constructor(private loaderService: LoaderService, private tosterService: ToastrService,
    private commonPropService: CommonPropsService, private vehicleSizeService: VehiclesizeService) { }
  ngOnInit() {
    this.fetchVehicleSizesAsync();
  }
  fetchVehicleSizesAsync() {
    this.loaderService.showLoader();
    this.vehicleSizeService.fetchVehiClesizesAsync().subscribe(response => {
      this.vehicleSizes = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as Vehiclesize[];
      this.filteredRows = this.vehicleSizes;
      this.loaderService.hideLoader();
    });
  }
  handleVehiclesizeChange(vehicleSize: Vehiclesize) {
    console.log(vehicleSize);
    this.loaderService.showLoader();
    const isAdd = (!vehicleSize.vehicleSizeId || vehicleSize.vehicleSizeId == 0) ? true : false;
    const isUpdate = vehicleSize.vehicleSizeId > 0;
    vehicleSize.isActive = true;
    this.insertOrUpdateVehiclesizeAsync(this.commonPropService.prepareModelForSave(vehicleSize), isAdd, isUpdate, false, false);
  }
  onCloseSidebar() {
    this.addEditVehiclesizeComponent.vehicleSize = this.newVehiclesize;
    this.addEditVehiclesizeComponent.hideSideBar();
    this.selectedVehiclesize = {} as Vehiclesize;
    this.isItemSelected = false;
  }
  onAdd() {
    console.log('Add button clicked in GridHeader');
    this.addEditVehiclesizeComponent.vehicleSize = this.newVehiclesize;
    this.addEditVehiclesizeComponent.showSidebar();
  }
  onEdit() {
    console.log('Edit button clicked in GridHeader');
    this.isItemSelected = false;
    this.addEditVehiclesizeComponent.vehicleSize = this.selectedVehiclesize;
    this.addEditVehiclesizeComponent.showSidebar();
    this.selectedVehiclesize = {} as Vehiclesize;
  }
  onDelete() {
    this.showDeleteConfirmModal = true;
  }
  confirmDelete() {
    this.loaderService.showLoader();
    this.selectedVehiclesize.isActive = false;
    this.insertOrUpdateVehiclesizeAsync(this.commonPropService.prepareModelForSave(this.selectedVehiclesize), false, false, false, true);
  }
  cancelDelete() {
    this.showDeleteConfirmModal = false;
  }
  onCopy() {
    this.showCopyConfirmModal = true;
  }
  confirmCopy() {
    this.loaderService.showLoader();
    this.selectedVehiclesize.vehicleSizeId = 0;
    this.insertOrUpdateVehiclesizeAsync(this.commonPropService.prepareModelForSave(this.selectedVehiclesize), false, false, true, false);
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
    this.vehicleSizes.forEach(row => row.isChecked = isChecked);
    if (isChecked) {
      this.selectedItems = this.vehicleSizes;
    } else {
      this.selectedItems = [];
    }
  }

  onRowCheckboxChange(row: any, event: any) {
    const allChecked = this.vehicleSizes.every(row => row.isChecked);
    const anyUnchecked = this.vehicleSizes.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
    if (row.isChecked) {
      this.selectedVehiclesize = row;
      this.isItemSelected = true;
      const multipleVehiclesizes: Vehiclesize[] = [];
      multipleVehiclesizes.push(row);
      this.addOrUpdateVehiclesizes(multipleVehiclesizes);
    } else {
      this.isItemSelected = false;
      this.selectedVehiclesize = {} as Vehiclesize;
      this.selectedItems = this.selectedItems.filter(item => item.vehicleSizeId !== row.vehicleSizeId);
    }
  }

  isAllChecked() {
    return this.vehicleSizes.every(row => row.isChecked);
  }
  addOrUpdateVehiclesizes(items: Vehiclesize[]): void {
    items.forEach(item => {
      const index = this.selectedItems.findIndex(existingItem => existingItem.vehicleSizeId === item.vehicleSizeId);
      if (index !== -1) {
        this.selectedItems[index] = { ...this.selectedItems[index], ...item };
      } else {
        this.selectedItems.push(item);
      }
    });
  }
  insertOrUpdateVehiclesizeAsync(vehiclesize: Vehiclesize, isAdd: boolean, isUpdate: boolean, isCopy: boolean, isDelete: boolean) {
    this.vehicleSizeService.insertOrUpdateVehiclesizeAsync(vehiclesize).subscribe(response => {
      if (response) {
        if (isAdd)
          this.tosterService.success("vehicle size added sucessful");
        else if (isUpdate)
          this.tosterService.success("vehicle size updated sucessful");
        else if (isCopy) {
          this.isItemSelected = false;
          this.selectedVehiclesize = {} as Vehiclesize;
          this.showCopyConfirmModal = false;
          this.tosterService.success("vehicle size copied sucessful");
        }
        else if (isDelete) {
          this.isItemSelected = false;
          this.selectedVehiclesize = {} as Vehiclesize;
          this.showDeleteConfirmModal = false;
          this.tosterService.success("policy workflow deleted sucessful");
        }
      }
      this.fetchVehicleSizesAsync();
      this.loaderService.hideLoader();
    });
  }
}
