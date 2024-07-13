import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UserProfile } from '../UserProfile';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit{
  email:string | undefined ;
  password:string | undefined;
  @Output() userLogin: EventEmitter<UserProfile> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }
  onSubmit(){
    const userProfile = {
      name: "",
      phoneNumber: 0,
      email: this.email,
      password: this.password,
      address: ""
  };
  console.log(userProfile);

  this.userLogin.emit(userProfile);

  }
}
