import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { PageEvent } from '@angular/material/paginator';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-streams',
  templateUrl: './streams.component.html',
  styleUrls: ['./streams.component.css']
})
export class StreamsComponent implements OnInit {
  platform_icon_twitch: string = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHg9IjBweCIgeT0iMHB4Igp3aWR0aD0iMTYiIGhlaWdodD0iMTYiCnZpZXdCb3g9IjAgMCAxNiAxNiI+CjxwYXRoIGQ9Ik0gMiAxIEwgMSAzLjY0ODQzOCBMIDEgMTIgTCA0IDEyIEwgNCAxNCBMIDUuMzc1IDE0IEwgNy44NDM3NSAxMiBMIDEwLjUgMTIgTCAxNCA4LjI1IEwgMTQgMSBaIE0gMyAyIEwgMTMgMiBMIDEzIDcuNzE0ODQ0IEwgMTAuODc1IDEwIEwgNy4yMDMxMjUgMTAgTCA1IDExLjg3NSBMIDUgMTAgTCAzIDEwIFogTSA2IDQgTCA2IDggTCA3IDggTCA3IDQgWiBNIDkgNCBMIDkgOCBMIDEwIDggTCAxMCA0IFoiPjwvcGF0aD4KPC9zdmc+";
  platform_icon_youtube: string = "https://www.svgrepo.com/show/156038/youtube.svg"

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
    this.UpdateData(this.getAPIURL(), this.pageSize, this.data?.page);
  }

  public getAPIURL() : string {
    let hostname : string = environment.CONCURRENCE_API_HOSTNAME;
    let port : number = environment.CONCURRENCE_API_PORT;
    let uri = `https://${hostname}:${port}`;

    console.log(uri);
    return uri;
  }

  public handlePageEvent(e: PageEvent) {
    // keep track of what page has which streams on it
    // by storing a list of streams in an array
    console.log(e);

    let previousPageIndex = e.previousPageIndex ? e.previousPageIndex : 0;
    let back : boolean = (previousPageIndex > e.pageIndex);
    let forward : boolean = (previousPageIndex < e.pageIndex);

    this.pageSize = e.pageSize;
    this.UpdateData(this.getAPIURL(), this.pageSize, this.data?.page, forward, back);
  };

  public handleSearchEvent(e: any){
    let value : String = e.srcElement.value.toLowerCase();
    let headers = {
      "Content-Type": "application/json",
      "method": "GET"
    };

    let search_type = "name";
    let search_parameters = `?`;
    let request_uri = `${this.getAPIURL()}/StreamSearch`;

    if(search_type == "name")
    {
      search_parameters = search_parameters + `user_login=${value}`;
    }

    request_uri = request_uri + search_parameters;

    this.http.get(request_uri, { headers })
      .subscribe((data: any) => {
        console.log(data)
        let response : APIResponse = {
          streams : data["streams"],
          length : data["streams"].length,
          page : data["streams"]
        };

        response.streams.forEach((v : Stream) => {
          v.creator_name = v.creator_name?.replace('{width}x{height}', `${this.thumbnail_width}x${this.thumbnail_height}`)
        });

        this.data = response;
    });
  }

  public RefreshData()
  {
    this.UpdateData(this.getAPIURL(), this.pageSize, this.data?.page);
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
          streams : data["streams"],
          length : data["streams"].length,
          page : data["streams"]
        };

        response.streams.forEach((v : Stream) => {
          v.thumbnail_img = v.thumbnail_img?.replace('{width}x{height}', `${this.thumbnail_width}x${this.thumbnail_height}`)
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
  creator_name  ?: string,
  game_name	    ?: string,
  type          ?: string,
  title	        ?: string,
  viewers       ?: number,
  dateTime      ?: string,
  language      ?: string,
  thumbnail_img ?: string,
  tag_ids       ?: string,
  //is_mature?: boolean,
  platform      ?: string
};
