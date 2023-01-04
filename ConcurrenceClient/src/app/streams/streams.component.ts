import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-streams',
  templateUrl: './streams.component.html',
  styleUrls: ['./streams.component.css']
})
export class StreamsComponent implements OnInit {

  public api_url: string = "https://localhost:5001";
  public platform: string = "Twitch";
  public title: string = "Streams";
  public platformStreamList?: Array<Stream> = [];
  public gridRowHeight = "1.5:1.25";
  public gridColumns = "4";

  public htmlHeader = this.platform + " " + this.title;
  public htmlFooter = "testing 2022";

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
      .subscribe((data: any) => {

        var streams: Array<Stream> = data["twitchStreams"];

        streams.forEach((v : Stream) => {
          const new_stream: Stream = {
            id: v.id,
            user_id: v.user_id,
            user_login: v.user_login,
            user_name: v.user_name,
            game_id: v.game_id,
            game_name: v.game_name,
            type: v.type,
            title: v.title,
            viewer_count: v.viewer_count,
            started_at: v.started_at,
            language: v.language,
            thumbnail_url: v.thumbnail_url?.replace('{width}x{height}', '400x190'),
            tag_ids: v.tag_ids,
            is_mature: v.is_mature,
            platform: "twitch"
          };

          this.platformStreamList?.push(new_stream);
        })
      });
  }
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
  is_mature?: boolean,
  platform?: string
}
