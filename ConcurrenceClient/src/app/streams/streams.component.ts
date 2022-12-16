import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatGridListModule } from '@angular/material/grid-list';

@Component({
  selector: 'app-streams',
  templateUrl: './streams.component.html',
  styleUrls: ['./streams.component.css']
})
export class StreamsComponent implements OnInit {

  public api_url: string = "https://localhost:5001";
  public platform = "Twitch";
  public title = "Streams";
  public streams?: PlatformStreamList;
  public gridRowHeight = "2:1";
  public gridColumns = "4";

  public constructor( private http : HttpClient ) { }

  public ngOnInit(): void {
    this.getResponse(this.api_url);
  }

  public getResponse(api_url : string) {
    var headers = {
      "Content-Type": "application/json",
      "method": "GET"
    };

    this.http.get(api_url, { headers })
      .subscribe((data: any) => this.streams = data);
  }
}

interface PlatformStreamList {
  "twitchStreams" ?: Array<Stream>;
}

interface Stream {
  id            ?: string,
  user_id	    ?: string,
  user_login    ?: string,
  user_name     ?: string,
  game_id       ?: number,
  game_name	    ?: string,
  type          ?: string,
  title	        ?: string,
  viewer_count  ?: number,	
  started_at    ?: string,
  language      ?: string,
  thumbnail_url ?: string,
  tag_ids       ?: string,
  is_mature	    ?: boolean
}
