import { Component, OnInit } from '@angular/core';
import { UserProfile } from '../UserProfile';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})

export class ListComponent implements OnInit{

  userProfile: UserProfile | any;
  ngOnInit(): void {
  }
  showUser(userProfile:UserProfile){
    console.log(userProfile); 
  }

}
