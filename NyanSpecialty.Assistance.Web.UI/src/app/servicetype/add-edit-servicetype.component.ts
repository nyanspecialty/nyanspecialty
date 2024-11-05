import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ServiceType } from '../models/servicetype';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-edit-servicetype',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-edit-servicetype.component.html',
  styleUrl: './add-edit-servicetype.component.css'
})
export class AddEditServiceTypeComponent implements OnInit {
  @Input() serviceType: ServiceType = {} as ServiceType; 

  @Output() serviceTypeChange = new EventEmitter<ServiceType>();

  isSidebarVisible = false;

  ngOnInit() {
    if (!this.serviceType) {
      this.serviceType = { serviceTypeId: 0, serviceName: '', description: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
    }
  }
  showSideBar(){
    this.isSidebarVisible= true;
    const backdrop = document.createElement('div');
    backdrop.className = 'modal-backdrop fade show';
    document.body.appendChild(backdrop);
  }
  closeSidebar() {
    this.isSidebarVisible = false;
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
      backdrop.remove();
    }
    this.resetForm();
  }
  resetForm() {
    this.serviceType = { serviceTypeId: 0, serviceName: '', description: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
  }
  onSubmit() {
    console.log('Form submitted', this.serviceType);
    this.serviceTypeChange.emit(this.serviceType);
    this.closeSidebar();
  }

  
}
