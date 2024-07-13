import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { User } from 'src/app/User';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {
  fullName:string | undefined ;
  address:string | undefined;
  @Output() userAdd: EventEmitter<User> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }
  onSubmit(){
    const user = {
      sno: 1,
      fullName: this.fullName,
      address: this.address,
      active: true
    }
    this.userAdd.emit(user);

  }

}
