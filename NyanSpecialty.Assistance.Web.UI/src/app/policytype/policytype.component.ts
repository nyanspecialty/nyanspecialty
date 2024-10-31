import { Component, OnInit } from '@angular/core';
import { Product } from './Product';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@Component({
  selector: 'app-policytype',
  standalone: true,
  imports: [NgxDatatableModule],
  templateUrl: './policytype.component.html',
  styleUrl: './policytype.component.css'
})
export class PolicytypeComponent implements OnInit {
  rows: Product[] = [];  // Array to hold product data
  filteredRows: Product[] = [];  // Array to hold filtered product data

  ngOnInit() {
    this.rows = [
      { product_name: 'DairyMilk', product_code: '4555' },
      { product_name: 'KitKat', product_code: '8885' },
      { product_name: 'Milk', product_code: '9696' },
      { product_name: 'Snickers', product_code: '1234' },
      { product_name: 'Mars Bar', product_code: '5678' },
      { product_name: 'Twix', product_code: '9101' },
      { product_name: 'Bounty', product_code: '1121' },
      { product_name: 'Milky Way', product_code: '3141' }
    ];
    
    // Initially set filteredRows to all rows
    this.filteredRows = this.rows;
  }

  // Method to filter rows based on the search term
  filterRows(searchTerm: string) {
    this.filteredRows = this.rows.filter(row => 
      row.product_name.toLowerCase().includes(searchTerm.toLowerCase())
    );
  }


  
}
