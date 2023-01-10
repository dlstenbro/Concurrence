import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import {PageEvent} from '@angular/material/paginator';

@Component({
  selector: 'app-streams',
  templateUrl: './streams.component.html',
  styleUrls: ['./streams.component.css']
})
export class StreamsComponent implements OnInit {

  api_url: string = "https://localhost:5001";
  platform: string = "Twitch";
  title: string = "Streams";

  data?: APIResponse = {} as APIResponse;

  gridRowHeight = "1.5:1.25";
  gridColumns = "5";

  htmlHeader = this.platform + " " + this.title;
  htmlFooter = "Concurrence 2023";

  length = 50;
  pageSize = 10;
  pageIndex = 0;
  pageSizeOptions = [5, 10, 25];

  hidePageSize = false;
  showPageSizeOptions = true;
  showFirstLastButtons = true;
  disabled = false;

  pageEvent : PageEvent | undefined;

  public constructor( private http : HttpClient ) { }

  public ngOnInit(): void {
    this.UpdateData(this.api_url, this.pageSize, this.data?.page);
  }

  handlePageEvent(e: PageEvent) {
    this.UpdateData(this.api_url, e.pageSize, this.data?.page);
  };

  public UpdateData(api_url : string, first : number = 20, after : string = "") {
    let headers = {
      "Content-Type": "application/json",
      "method": "GET"
    };

    let request_uri = `${api_url}`
    let paramFirst = `first=${first}`

    request_uri = `${request_uri}?${paramFirst}`

    if(after)
    {
      request_uri = `${request_uri}&after=${after}`
    }

    this.http.get(request_uri, { headers })
      .subscribe((data: any) => {

        let response : APIResponse = {
          streams : data["data"]["twitchStreams"],
          length : data["data"].length,
          page : data["page"]
        }

        response.streams.forEach((v : Stream) => {
          v.thumbnail_url = v.thumbnail_url?.replace('{width}x{height}', '400x190')
        });

        this.data = response;
    });
  }
}


interface APIResponse {
  streams : Array<Stream>,
  length: number,
  page ?: string
}

interface Stream {
  id            ?: string,
  user_id	      ?: string,
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

