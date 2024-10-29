import { Directive, HostListener, ElementRef, Renderer2 } from '@angular/core';

@Directive({
  selector: '[togglePassword]',
  standalone: true // Make the directive standalone
})
export class TogglePasswordDirective {
  private show: boolean = false;

  constructor(private el: ElementRef, private renderer: Renderer2) {}

  @HostListener('click') onClick(): void {
    this.show = !this.show;

    const inputGroup = this.el.nativeElement.closest('.input-group');
    const inputEl = inputGroup ? inputGroup.querySelector('input') : null;
    const iconEl = this.el.nativeElement.querySelector('i');

    if (inputEl) {
      if (this.show) {
        this.renderer.setAttribute(inputEl, 'type', 'text');
        this.renderer.removeClass(iconEl, 'fa-eye-slash');
        this.renderer.addClass(iconEl, 'fa-eye');
      } else {
        this.renderer.setAttribute(inputEl, 'type', 'password');
        this.renderer.removeClass(iconEl, 'fa-eye');
        this.renderer.addClass(iconEl, 'fa-eye-slash');
      }
    } else {
      console.error('Input element not found!');
    }
  }
}
