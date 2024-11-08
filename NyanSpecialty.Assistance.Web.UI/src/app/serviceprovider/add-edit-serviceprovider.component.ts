import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ServiceProvider } from '../models/serviceprovider';

@Component({
  selector: 'add-edit-serviceprovider',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './add-edit-serviceprovider.component.html',
  styleUrl: './add-edit-serviceprovider.component.css'
})
export class AddEditServiceProviderComponent {
  isSidebarVisible: boolean = false;

  @Input() serviceProvider: ServiceProvider = {} as ServiceProvider;

  @Output() serviceProviderChange = new EventEmitter<ServiceProvider>();

  @Output() cancel = new EventEmitter<void>();

  ngOnInit() {
    if (!this.serviceProvider) {
      this.serviceProvider = { providerId: 0, name: '', email: '',contactNumber:'',address:'',availabilityStatus:'',latitude:'',longitude:'',rating:'',serviceArea:'', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
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
    this.serviceProvider = { providerId: 0, name: '', email: '',contactNumber:'',address:'',availabilityStatus:'',latitude:'',longitude:'',rating:'',serviceArea:'', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
  }
  onSubmit() {
    console.log('Form submitted', this.serviceProvider);
    this.serviceProviderChange.emit(this.serviceProvider);
    this.closeSidebar();
  }
}
