import { Component, EventEmitter, Input, Output } from '@angular/core';
import { VehicleClass } from '../models/vehicleclass';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'add-edit-vehicleclass',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-edit-vehicleclass.component.html',
  styleUrl: './add-edit-vehicleclass.component.css'
})
export class AddEditVehicleClassComponent {

  isSidebarVisible: boolean = false;

  @Input() vehicleClass: VehicleClass = {} as VehicleClass;

  @Output() vehicleClassChange = new EventEmitter<VehicleClass>();

  @Output() cancel = new EventEmitter<void>();

  ngOnInit(): void {
    if (!this.vehicleClass) {
      this.vehicleClass = { vehicleClassId: 0, name: '', code: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
    }
  }
  showSidebar() {
    this.isSidebarVisible = true;
    const backdrop = document.createElement('div');
    backdrop.className = 'modal-backdrop fade show';
    document.body.appendChild(backdrop);
  }

  closeSidebar() {
    this.cancel.emit();
  }
  hideSideBar() {
    this.isSidebarVisible = false;
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
      backdrop.remove();
    }
    this.resetForm();
  }
  resetForm() {
    this.vehicleClass = { vehicleClassId: 0, name: '', code: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
  }
  onSubmit() {
    console.log('Form submitted', this.vehicleClass);
    this.vehicleClassChange.emit(this.vehicleClass);
    this.resetForm();
    this.closeSidebar();
  }
}
