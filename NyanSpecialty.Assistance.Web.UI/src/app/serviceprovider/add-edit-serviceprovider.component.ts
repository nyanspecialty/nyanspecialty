import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ServiceProvider } from '../models/serviceprovider';
import * as L from 'leaflet';

@Component({
  selector: 'add-edit-serviceprovider',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-edit-serviceprovider.component.html',
  styleUrl: './add-edit-serviceprovider.component.css'
})
export class AddEditServiceProviderComponent implements AfterViewInit {

  isSidebarVisible: boolean = false;

  isMapModalVisible: boolean = false;

  @Input() serviceProvider: ServiceProvider = {} as ServiceProvider;

  @Output() serviceProviderChange = new EventEmitter<ServiceProvider>();

  @Output() cancel = new EventEmitter<void>();

  map: any;

  marker: any;

  searchQuery: string = '';

  selectedLocation: any = { address: '', lat: 0, lng: 0 };

  ngOnInit() {
    if (!this.serviceProvider) {
      this.serviceProvider = { providerId: 0, name: '', email: '', contactNumber: '', address: '', availabilityStatus: '', latitude: '', longitude: '', rating: '', serviceArea: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
    }
  }
  ngAfterViewInit() {
    this.initMap();
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
    this.serviceProvider = { providerId: 0, name: '', email: '', contactNumber: '', address: '', availabilityStatus: '', latitude: '', longitude: '', rating: '', serviceArea: '', createdOn: new Date(), modifiedOn: new Date(), isActive: true };
  }
  onSubmit() {
    console.log('Form submitted', this.serviceProvider);
    this.serviceProviderChange.emit(this.serviceProvider);
    this.closeSidebar();
  }
  openMapModal() {
    this.isMapModalVisible = true;
    setTimeout(() => {
      this.map.invalidateSize(); // Ensure the map renders properly in a modal
    }, 100);
  }

  closeMapModal() {
    this.isMapModalVisible = false;
  }

  initMap() {

    const defaultIcon = L.icon({
      iconUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon.png',
      shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-shadow.png',
      iconSize: [25, 41],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      shadowSize: [41, 41]
    });

    L.Marker.prototype.options.icon = defaultIcon;
    this.map = L.map('map').setView([37.7749, -122.4194], 13); // Default location

    // Add OpenStreetMap tiles
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; OpenStreetMap contributors',
    }).addTo(this.map);

    // Add draggable marker
    this.marker = L.marker([37.7749, -122.4194], { draggable: true }).addTo(this.map);

    // Update location on marker drag
    this.marker.on('dragend', () => {
      const position = this.marker.getLatLng();
      this.updateLocation(position.lat, position.lng);
    });
  }

  updateLocation(lat: number, lng: number) {
    this.selectedLocation.lat = lat;
    this.selectedLocation.lng = lng;

    // Reverse geocode to get address
    fetch(`https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${lng}`)
      .then((response) => response.json())
      .then((data) => {
        this.selectedLocation.address = data.display_name || 'Unknown address';
        this.selectedLocation.serviceArea=data;
      });
  }

  searchLocation() {
    if (!this.searchQuery) {
      alert('Please enter a search query!');
      return;
    }

    // Search for location using geocoding
    fetch(`https://nominatim.openstreetmap.org/search?format=json&q=${this.searchQuery}`)
      .then((response) => response.json())
      .then((results) => {
        if (results.length > 0) {
          const { lat, lon, display_name } = results[0];
          this.map.setView([lat, lon], 15);
          this.marker.setLatLng([lat, lon]);
          this.updateLocation(lat, lon);
        } else {
          alert('Location not found!');
        }
      });
  }

  confirmLocation() {
    // Update the input field and Angular model
    this.serviceProvider.address=this.selectedLocation.address;
    this.serviceProvider.longitude  =this.selectedLocation.lng;
    this.serviceProvider.latitude=this.selectedLocation.lat;
    this.serviceProvider.serviceArea=this.selectedLocation.serviceArea.address.country;
    this.closeMapModal();
  }
}
