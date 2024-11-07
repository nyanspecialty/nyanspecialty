import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Vehiclesize } from '../models/vehiclesize';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'add-edit-vehiclesize',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-edit-vehiclesize.component.html',
  styleUrl: './add-edit-vehiclesize.component.css'
})
export class AddEditVehiclesizeComponent implements OnInit {
  isSidebarVisible: boolean = false;

  @Input() vehicleSize: Vehiclesize = {} as Vehiclesize;

  @Output() vehiclesizeChange = new EventEmitter<Vehiclesize>();

  @Output() cancel = new EventEmitter<void>();

  ngOnInit(): void {
    if (!this.vehicleSize) {
      this.vehicleSize = { vehicleSizeId: 0, name: '', code: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
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
    this.vehicleSize = { vehicleSizeId: 0, name: '', code: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
  }
  onSubmit() {
    console.log('Form submitted', this.vehicleSize);
    this.vehiclesizeChange.emit(this.vehicleSize);
    this.resetForm();
    this.closeSidebar();
  }
}
