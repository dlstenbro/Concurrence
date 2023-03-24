import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { PageEvent } from '@angular/material/paginator';

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

  gridRowHeight = "1.25:1.75";
  gridColumns = "5";

  htmlHeader = this.title;
  htmlFooter = "Concurrence 2023";

  length = 500;
  pageIndex = 0;
  pageSizeOptions = [15, 30, 45 ];
  pageSize = this.pageSizeOptions[0];

  hidePageSize = false;
  showPageSizeOptions = true;
  showFirstLastButtons = false;
  disabled = false;

  pageEvent : PageEvent | undefined;

  thumbnail_width : number = 375;
  thumbnail_height : number = 200;

  changeCount : number = 0;

  public constructor( private http : HttpClient ) { }

  public ngOnInit(): void {
    this.UpdateData(this.api_url, this.pageSize, this.data?.page);
  }

  public handlePageEvent(e: PageEvent) {
    // keep track of what page has which streams on it
    // by storing a list of streams in an array
    console.log(e);

    let previousPageIndex = e.previousPageIndex ? e.previousPageIndex : 0;
    let back : boolean = false;
    let forward : boolean = false;

    if(previousPageIndex > e.pageIndex )
    {
      back = true;
    }
    else if(previousPageIndex < e.pageIndex)
    {
      forward = true;
    }
    this.pageSize = e.pageSize;
    this.UpdateData(this.api_url, this.pageSize, this.data?.page, forward, back);
  };

  public handleSearchEvent(e: any){
    let value : String = e.srcElement.value.toLowerCase();
    let headers = {
      "Content-Type": "application/json",
      "method": "GET"
    };

    let search_type = "name";

    let search_parameters = `?`;
    let request_uri = `${this.api_url}/StreamSearch`;

    if(search_type == "name")
    {
      search_parameters = search_parameters + `user_login=${value}`;
    }

    request_uri = request_uri + search_parameters;

    this.http.get(request_uri, { headers })
      .subscribe((data: any) => {
        console.log(data)
        let response : APIResponse = {
          streams : data["data"]["twitchStreams"],
          length : data["data"].length,
          page : data["page"]
        };

        response.streams.forEach((v : Stream) => {
          v.thumbnail_url = v.thumbnail_url?.replace('{width}x{height}', `${this.thumbnail_width}x${this.thumbnail_height}`)
        });

        this.data = response;
    });
  }

  public RefreshData()
  {
    this.UpdateData(this.api_url, this.pageSize, this.data?.page);
  }

  public UpdateData(api_url : string, first : number = 20, cursor : string = "", isForward : boolean = false, isBackward : boolean = false) {
    let headers = {
      "Content-Type": "application/json",
      "method": "GET"
    };

    let request_uri = `${api_url}`;
    let paramFirst = `first=${first}`;

    request_uri = `${request_uri}?${paramFirst}`;

    if(isForward) { request_uri = `${request_uri}&after=${cursor}`; }
    else if(isBackward){ request_uri = `${request_uri}&before=${cursor}`; }

    this.http.get(request_uri, { headers })
      .subscribe((data: any) => {
        console.log(data)
        let response : APIResponse = {
          streams : data["data"]["twitchStreams"],
          length : data["data"].length,
          page : data["page"]
        };

        response.streams.forEach((v : Stream) => {
          v.thumbnail_url = v.thumbnail_url?.replace('{width}x{height}', `${this.thumbnail_width}x${this.thumbnail_height}`)
        });

        this.data = response;
    });
  }
};

type APIResponse = {
  streams : Array<Stream>,
  length: number,
  page ?: string
};

type Stream = {
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
};

