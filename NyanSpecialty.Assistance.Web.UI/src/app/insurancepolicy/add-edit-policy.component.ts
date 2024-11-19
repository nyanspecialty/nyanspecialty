import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InsurancePolicy } from '../models/insurancepolicy';
import { VehicleClass } from '../models/vehicleclass';
import { VehicleClassService } from '../services/vehicleclass.service';
import { VehiclesizeService } from '../services/vehiclesize.service';
import { PolicyTypeService } from '../services/policytype.service';
import { PolicyCategoryService } from '../services/policycategory.service';
import { forkJoin } from 'rxjs';
import { Vehiclesize } from '../models/vehiclesize';
import { PolicyType } from '../models/policytype';
import { PolicyCategory } from '../models/policycategory';


@Component({
  selector: 'add-edit-policy',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-edit-policy.component.html',
  styleUrl: './add-edit-policy.component.css'
})
export class AddEditPolicyComponent implements OnInit, AfterViewInit {

  constructor(private vehicleClassService: VehicleClassService,
    private vehicleSizeService: VehiclesizeService,
    private policyTypeService: PolicyTypeService,
    private policyCategoryService: PolicyCategoryService
  ) { }

  isSidebarVisible: boolean = false;

  @Input() insurancePolicy: InsurancePolicy = {} as InsurancePolicy;

  @Output() insurancePolicyChange = new EventEmitter<InsurancePolicy>();

  @Output() cancel = new EventEmitter<void>();

  vehicleClasses: VehicleClass[] = [];

  vehicleSizes: Vehiclesize[] = [];

  policyTypes: PolicyType[] = [];

  policyCategorgory: PolicyCategory[] = [];

  educations: any[] = [];

  employementStatuses: any[] = [];

  maritalStatus: any[] = [];

  coverages: any[] = [];

  ngOnInit() {
    if (!this.insurancePolicy) {
      this.clearform();

    }
  }
  ngAfterViewInit() {
    forkJoin({
      _vehicleClasses: this.vehicleClassService.fetchVehicleClassesAsync(),
      _vehicleSizes: this.vehicleSizeService.fetchVehiClesizesAsync(),
      _policyTypes: this.policyTypeService.getAllPolicyTypeAsync(),
      _policyCategory: this.policyCategoryService.fetchPolicyCategoriesAsync()
    }).subscribe({
      next: (results) => {
        this.vehicleClasses = results._vehicleClasses;
        this.vehicleSizes = results._vehicleSizes;
        this.policyTypes = results._policyTypes;
        this.policyCategorgory = results._policyCategory;
        this.educations = [
          { "name": "High School or Below", "code": "HS" },
          { "name": "Bachelor", "code": "BA" },
          { "name": "Master", "code": "MA" },
          { "name": "Doctor", "code": "DO" },
          { "name": "College", "code": "COL" }
        ];
        this.employementStatuses = [
          { "name": "Employed", "code": "EMP" },
          { "name": "Unemployed", "code": "UNEMP" },
          { "name": "Medical Leave", "code": "MED" },
          { "name": "Disabled", "code": "DIS" },
          { "name": "Retired", "code": "RET" }
        ];

        this.maritalStatus = [
          { "name": "Divorced", "code": "DIV" },
          { "name": "Married", "code": "MAR" },
          { "name": "Single", "code": "SIN" },
          { "name": "Married GIM", "code": "MAR_GIM" }
        ];

        this.coverages = [
          { "name": "Extended", "code": "EXT" },
          { "name": "Basic", "code": "BAS" },
          { "name": "Premium", "code": "PRE" }
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
    if (!this.insurancePolicy) {
      this.clearform();
    }
  }
  onSubmit() {
    console.log('Form submitted', this.insurancePolicy);
    this.insurancePolicyChange.emit(this.insurancePolicy);
    this.closeSidebar();
  }
  clearform() {
    if (!this.insurancePolicy) {
      this.insurancePolicy = {
        insurancePolicyId: 0,
        policyReference: '',
        vin: '',
        carRegistrationNo: '',
        vehicleClass: '',
        vehicleSize: '',
        customerName: '',
        customerPhone: '',
        customerEmail: '',
        customerLifetimeValue: undefined,
        gender: undefined,
        education: '',
        employmentStatus: '',
        income: undefined,
        maritalStatus: '',
        policyType: '',
        policy: '',
        coverage: '',
        effectiveToDate: undefined,
        monthlyPremiumAuto: undefined,
        monthsSinceLastClaim: undefined,
        monthsSincePolicyInception: undefined,
        numberOfOpenComplaints: undefined,
        numberOfPolicies: undefined,
        renewOfferType: '',
        salesChannel: '',
        totalClaimAmount: undefined,
        state: '',
        locationCode: '',
        response: '',
        isActive: true,
        createdOn: new Date(),
        modifiedOn: new Date()
      };
    }
  }
}
