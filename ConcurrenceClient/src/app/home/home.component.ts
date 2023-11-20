import { Component, Input } from '@angular/core';
import { StreamsComponent } from '../streams/streams.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  @Input() streamComponent ?: StreamsComponent;
}
