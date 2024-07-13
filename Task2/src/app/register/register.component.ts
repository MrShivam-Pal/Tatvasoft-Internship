import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UserProfile } from '../UserProfile';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit{
  name: string | undefined;
  phoneNumber: number | undefined;
  email: string | undefined;
  password: string | undefined;
  address: string | undefined;
  @Output() userRegister: EventEmitter<UserProfile> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }
  onSubmit(){
      const userProfile = {
        name: this.name,
        phoneNumber: this.phoneNumber,
        email: this.email,
        password: this.password,
        address: this.address
    };
    console.log(userProfile);

    this.userRegister.emit(userProfile);
  }

}
