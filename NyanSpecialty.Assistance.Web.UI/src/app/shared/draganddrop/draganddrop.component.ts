import { Component, EventEmitter, Output } from '@angular/core';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-draganddrop',
  standalone: true,
  imports: [],
  templateUrl: './draganddrop.component.html',
  styleUrl: './draganddrop.component.css'
})
export class DraganddropComponent {
  @Output() fileDataEmitter = new EventEmitter<any[]>(); // Emit file data to parent
  
  fileData: any[] = [];

  onFileChange(event: any) {
    const file = event.target.files[0];
    this.readExcelFile(file);
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
    // Add visual feedback for drag over
  }

  onDragLeave(event: DragEvent) {
    // Remove visual feedback for drag leave
  }

  onDrop(event: DragEvent) {
    event.preventDefault();
    const file = event.dataTransfer?.files[0];
    if (file)
      this.readExcelFile(file);
  }

  readExcelFile(file: File) {
    if (file && (file.type === "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" ||
      file.type === "application/vnd.ms-excel")) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        const data = new Uint8Array(e.target.result);
        const workbook = XLSX.read(data, { type: 'array' });
        const sheetName = workbook.SheetNames[0];
        const sheet = workbook.Sheets[sheetName];
        this.fileData = XLSX.utils.sheet_to_json(sheet);
        this.displayFileInfo();
      };
      reader.readAsArrayBuffer(file);
    } else {
      alert("Please upload a valid Excel file.");
    }
  }

  displayFileInfo() {
    // Emit the data to the parent component
    this.fileDataEmitter.emit(this.fileData);
  }

  upload() {
    // Implement your upload logic here
    console.log("Upload functionality not implemented.");
  }
  onclose(){
    console.log("Upload functionality not implemented.");
  }
}
