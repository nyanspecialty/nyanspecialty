import { Component, OnInit } from '@angular/core';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { PolicyTypeService } from '../services/policytype.service';
import { PolicyType } from '../models/policytype';

@Component({
  selector: 'app-policytype',
  standalone: true,
  imports: [NgxDatatableModule],
  templateUrl: './policytype.component.html',
  styleUrl: './policytype.component.css'
})
export class PolicytypeComponent implements OnInit {
  constructor(private policyTypeService: PolicyTypeService) { }
  rows: PolicyType[] = [];
  filteredRows: PolicyType[] = [];
  ngOnInit() {
    this.fetchPolicyTypesAsync();
  }
  public fetchPolicyTypesAsync() {
    this.policyTypeService.getAllPolicyTypeAsync().subscribe(response => {
      this.rows = response.Value;
      this.filteredRows = this.rows;
    });
  }
}
