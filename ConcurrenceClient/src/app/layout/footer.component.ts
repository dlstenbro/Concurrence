import { Component } from '@angular/core';

@Component({
  selector: 'app-layout-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class LayoutFooterComponent {
  public appName : string = 'Concurrence';
  public date ?: string;

  constructor() {
    this.date = this.getDate();
  }

   public getDate = (d = new Date()) => `${d.getFullYear()}-${d.getMonth()}-${d.getDay()}`;
}
