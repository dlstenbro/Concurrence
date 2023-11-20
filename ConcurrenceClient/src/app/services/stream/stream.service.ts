import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, lastValueFrom } from 'rxjs';

// models
import { APIResponse } from 'src/app/models/apiresponse.model';

// environment
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StreamService {
  constructor(private httpclient : HttpClient) {  }

  private getAPIURL = () => `${environment.CONCURRENCE_API_HOSTNAME}:${environment.CONCURRENCE_API_PORT}`;

  public getStreams$ = () => this.httpclient.get<APIResponse>(this.getAPIURL()) as Observable<APIResponse>;
  /*
  public getStreams$(first : number = 20, cursor : string = "", isForward : boolean = false, isBackward : boolean = false) : Observable<APIResponse> {

    let request_uri = `${this.api_endpoint}?first=${first}`;
    request_uri = isForward ? `${request_uri}&after=${cursor}` : request_uri;
    request_uri = isBackward ? `${request_uri}&before=${cursor}` : request_uri;

    this.httpclient.get<APIResponse>(request_uri).subscribe((data : APIResponse) => { this.streams = data });
    return of(this.streams);
  }
  */
}
