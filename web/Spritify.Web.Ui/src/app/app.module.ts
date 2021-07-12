import { APP_BASE_HREF } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
      // TODO improve this mess
      { provide: APP_BASE_HREF, useValue: (window as any)["baseHref"] }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
