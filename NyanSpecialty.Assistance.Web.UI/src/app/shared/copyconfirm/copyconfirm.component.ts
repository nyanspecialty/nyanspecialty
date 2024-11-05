import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-copyconfirm',
  standalone: true,
  imports: [],
  templateUrl: './copyconfirm.component.html',
  styleUrl: './copyconfirm.component.css'
})
export class CopyconfirmComponent {
  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  onConfirm() {
    this.confirm.emit(); // Emit confirm event
  }

  onCancel() {
    this.cancel.emit(); // Emit cancel event
  }
}
