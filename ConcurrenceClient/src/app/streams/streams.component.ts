import { Component, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-streams',
  templateUrl: './streams.component.html',
  styleUrls: ['./streams.component.css']
})
export class StreamsComponent implements OnInit {

  public api_url: string = "http://localhost:80";
  public platform = "Twitch";
  public title = "Streams";
  public streams : Streams | undefined;

  constructor(private http : HttpClient) {

    // get streams from API
    var headers = {
      "Allow-Access-Control-Origin" : "*",
      "Content-Type"                : "application/json",
      "method"                      : "GET"
    };

    var request = http.get(this.api_url, { headers }).subscribe((data: any) => {
      console.log(data);
    });



    //this.streams = this.GetReponse(this.api_url).then(res => res.parse("twitchStreams"));
    //var response = this.GetResponse(this.api_url);
    //console.log(response);
  }

  ngOnInit(): void {

  }

/*
  GetResponse(url: string): Promise<any> {
    var headers = { "Content-Type": "application/json", "method": "GET" };
    return fetch(url, { mode : "no-cors", headers : headers })
      .then(res => res.json())
      .catch(error => {
        console.log(error)
      });
  }
  */
}

interface Streams {
  "twitchStreams" : any;
}

interface Stream {
  id            : string,
  user_id	    : string,
  user_login    : string,
  user_name     : string,
  game_id       : number,
  game_name	    : string,
  type          : string,
  title	        : string,
  viewer_count  : number,	
  started_at    : string,
  language      : string,
  thumbnail_url : string,
  tag_ids       : string,
  is_mature	    : boolean
}
