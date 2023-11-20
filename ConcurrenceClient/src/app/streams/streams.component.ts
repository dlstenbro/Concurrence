import { Component, OnDestroy, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { Observable, Subscription, tap } from 'rxjs';
import { map } from 'rxjs/operators';

// models
import { APIResponse } from 'src/app/models/apiresponse.model';

// services
import { StreamService } from '../services/stream/stream.service';

@Component({
  selector: 'app-streams',
  templateUrl: './streams.component.html',
  styleUrls: ['./streams.component.css']
})
export class StreamsComponent implements OnInit, OnDestroy {
  dataObservable$ ?: Observable<APIResponse>;

  subscriptions = new Subscription();

  gridRowHeight = "1.25:1.75";
  gridColumns = "5";

  length = 500;
  pageIndex = 0;
  pageSizeOptions = [15, 30, 45];
  pageSize = this.pageSizeOptions[0];

  hidePageSize = false;
  showPageSizeOptions = true;
  showFirstLastButtons = false;
  disabled = false;

  pageEvent : PageEvent | undefined;
  changeCount : number = 0;

  thumbnail_width : number = 375;
  thumbnail_height : number = 200;

  public constructor( private streamService: StreamService ) { }

  public ngOnInit(): void {
    console.log("ngOnInit")

    let observer = {
      next : (r : APIResponse) => {
        console.log(`observer received: ${r}`);
        r.streams.forEach((e) => {
          e.thumbnail_img?.replace('{width}x{height}',`${this.thumbnail_width}x${this.thumbnail_height}`);
        });
      },
      error : (e : Error) => console.log(`Error fetching request: ${e}`) ,
      completed : () => {}
    }

    this.dataObservable$ = this.streamService.getStreams$();
    this.dataObservable$.pipe(
                        tap((data) => {
                          console.log(`pipe received ${data.streams}`)
                        }),
                        map(data => {
                          data.streams.forEach((e) => {
                            e.thumbnail_img?.replace('{width}x{height}',`${this.thumbnail_width}x${this.thumbnail_height}`);
                            console.log(`observer received: ${e}`);
                          });
                        })
                      )
                      .subscribe();

    //this.streamService.getStreams$()
    //                  .pipe( tap((data) => console.log(`pipe received ${data.streams}`)) )
    //                  .subscribe(observer)
    //this.subscriptions.add(obs$);

    //this.streamService.getStreams$(this.pageSize, this.data$?.page)
  }

  public ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  public handlePageEvent(e: PageEvent) {
    // keep track of what page has which streams on it
    // by storing a list of streams in an array
    let previousPageIndex : number = e.previousPageIndex ? e.previousPageIndex : 0;
    let back : boolean = (previousPageIndex > e.pageIndex);
    let forward : boolean = (previousPageIndex < e.pageIndex);

    this.pageSize = e.pageSize;
    ///this.streamService.getStreams$(this.pageSize, this.data?.page, forward, back).subscribe( s => this.data = s );
  };
}
