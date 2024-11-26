import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { InsurancePolicyService } from '../services/insurancepolicy.service';
import { InsurancePolicy } from '../models/insurancepolicy';
import { Case } from '../models/case';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ServiceTypeService } from '../services/servicetype.service';
import { forkJoin } from 'rxjs';
import { ServiceType } from '../models/servicetype';
import * as L from 'leaflet';

@Component({
  selector: 'app-caseaddedit',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './caseaddedit.component.html',
  styleUrl: './caseaddedit.component.css',
  encapsulation: ViewEncapsulation.Emulated
})
export class AddEditCaseComponent implements OnInit, AfterViewInit {

  serviceTypes: ServiceType[] = [];

  coreInsurencePolycies: InsurancePolicy[] = [];

  priorityTypes: any[] = [];

  isSidebarVisible: boolean = false;

  @Input() caseDetails: Case = {} as Case;

  @Output() caseChange = new EventEmitter<Case>();

  @Output() cancel = new EventEmitter<void>();

  isMapModalVisible = false;

  map: any;

  marker: any;

  searchQuery: string = '';

  selectedLocation: any = { address: '', lat: 0, lng: 0 };


  constructor(private insurencePolicyService: InsurancePolicyService,
    private serviceTypeService: ServiceTypeService) { }

  ngOnInit() {
    if (!this.caseDetails) {
      this.resetForm();
    }
    this.loadMainData();
  }
  ngAfterViewInit() {
    this.initMap();
  }
  loadMainData() {
    forkJoin({
      _coreInsurencePolycies: this.insurencePolicyService.fetchInsurancePolicysAsync(),
      _serviceTypes: this.serviceTypeService.fetchServiceTypesAsync(),

    }).subscribe({
      next: (results) => {
        this.coreInsurencePolycies = results._coreInsurencePolycies.slice(0, 200);
        this.serviceTypes = results._serviceTypes;

        this.priorityTypes = [
          { "priorityTypeId": 1, "name": "High", "code": "High" },
          { "priorityTypeId": 2, "name": "Low", "code": "Low" },
          { "priorityTypeId": 3, "name": "Medium", "code": "Medium" }
        ];

      },
      error: (err) => {
        console.error('Error fetching dropdown data', err);
      }
    });
  }
  showSidebar() {
    this.isSidebarVisible = true;
    const backdrop = document.createElement('div');
    backdrop.className = 'modal-backdrop fade show';
    document.body.appendChild(backdrop);
  }

  closeSidebar() {
    this.cancel.emit();
    this.resetForm();
  }
  hideSideBar() {
    this.isSidebarVisible = false;
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
      backdrop.remove();
    }
    this.resetForm();
  }

  onSubmit() {
    console.log('Form submitted', this.caseDetails);
    this.caseChange.emit(this.caseDetails);
    this.closeSidebar();
  }

  resetForm() {
    this.caseDetails = {
      caseId: 0,
      insurancePolicyId: 0,
      description: '',
      customerName: '',
      phone: '',
      email: '',
      currentLocation: '',
      langitude: '',
      latitude: '',
      serviceTypeId: 0,
      statusId: 1,
      serviceProviderId: 0,
      serviceRequestDate: new Date(),
      responseTime: new Date(),
      completionTime: new Date(),
      rating: 0,
      feedback: '',
      priority: 0,
      createdOn: new Date(),
      modifiedOn: new Date(),
      isActive: true
    };
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
    this.caseDetails.currentLocation = this.selectedLocation.address;
    this.caseDetails.langitude = this.selectedLocation.lng;
    this.caseDetails.latitude = this.selectedLocation.lat;
    this.closeMapModal();
  }
  verifyInsuranceDetails(event: any) {
    if (this.caseDetails.insurancePolicyId) {
      const insurancePolicyId = Number(this.caseDetails.insurancePolicyId);
      if (!isNaN(insurancePolicyId)) {
        const policyDetails = this.coreInsurencePolycies.find(x => x.insurancePolicyId === insurancePolicyId);
        if (policyDetails) {
          this.caseDetails.customerName = policyDetails.customerName;
          this.caseDetails.phone = policyDetails.customerPhone;
          this.caseDetails.email = policyDetails.customerEmail;
        } else {
          console.warn('No policy found with the ID:', insurancePolicyId);
        }
      } else {
        console.error('Invalid insurancePolicyId:', this.caseDetails.insurancePolicyId);
      }
    }
  }
}
