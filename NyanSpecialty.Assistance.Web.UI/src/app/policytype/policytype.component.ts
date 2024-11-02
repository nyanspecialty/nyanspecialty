import { ChangeDetectorRef, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { PolicyTypeService } from '../services/policytype.service';
import { PolicyType } from '../models/policytype';
import { LoaderService } from '../services/loader.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-policytype',
  standalone: true,
  imports: [CommonModule, NgxDatatableModule, FormsModule],
  templateUrl: './policytype.component.html',
  styleUrl: './policytype.component.css',
  encapsulation: ViewEncapsulation.None
})
export class PolicytypeComponent implements OnInit {
  rows: PolicyType[] = [];
  filteredRows: PolicyType[] = [];
  filters: any = {};
  headerCheckboxChecked: boolean = false;

  constructor(private policyTypeService: PolicyTypeService, private loader: LoaderService) { }

  ngOnInit() {
    this.fetchPolicyTypesAsync();
  }

  public fetchPolicyTypesAsync() {
    this.policyTypeService.getAllPolicyTypeAsync().subscribe(response => {
      this.rows = response.map((item: any) => ({
        ...item,
        isChecked: false
      })) as PolicyType[];
      this.filteredRows = this.rows;
    });
  }
  toggleAll(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.rows.forEach(row => row.isChecked = isChecked);
  }

  onRowCheckboxChange() {
    const allChecked = this.rows.every(row => row.isChecked);
    const anyUnchecked = this.rows.some(row => !row.isChecked);
    if (allChecked) {
      this.headerCheckboxChecked = true;
    } else if (anyUnchecked) {
      this.headerCheckboxChecked = false;
    }
  }

  isAllChecked() {
    return this.rows.every(row => row.isChecked);
  }
}