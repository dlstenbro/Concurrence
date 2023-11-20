import { Component, Input } from "@angular/core";

// model
import { Stream } from "../models/streams.model";

@Component({
  selector: 'app-stream-detail',
  templateUrl: './stream-detail.component.html',
  styleUrls: ['./stream-detail.component.css']
})
export class StreamDetailComponent {
  @Input() stream?: Stream;

  platform_icon_twitch: string = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHg9IjBweCIgeT0iMHB4Igp3aWR0aD0iMTYiIGhlaWdodD0iMTYiCnZpZXdCb3g9IjAgMCAxNiAxNiI+CjxwYXRoIGQ9Ik0gMiAxIEwgMSAzLjY0ODQzOCBMIDEgMTIgTCA0IDEyIEwgNCAxNCBMIDUuMzc1IDE0IEwgNy44NDM3NSAxMiBMIDEwLjUgMTIgTCAxNCA4LjI1IEwgMTQgMSBaIE0gMyAyIEwgMTMgMiBMIDEzIDcuNzE0ODQ0IEwgMTAuODc1IDEwIEwgNy4yMDMxMjUgMTAgTCA1IDExLjg3NSBMIDUgMTAgTCAzIDEwIFogTSA2IDQgTCA2IDggTCA3IDggTCA3IDQgWiBNIDkgNCBMIDkgOCBMIDEwIDggTCAxMCA0IFoiPjwvcGF0aD4KPC9zdmc+";
  platform_icon_youtube: string = "https://www.svgrepo.com/show/156038/youtube.svg"
}
