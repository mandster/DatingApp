import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  ViewChild,
  ElementRef
} from '@angular/core';
import { User, UserResult } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { MemberListComponent } from '../member-list/member-list.component';
import { currentId } from 'async_hooks';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() user: User;
  currentUserLikees = [];
  currentUserLikeesFiltered = [];

  // tslint:disable-next-line: no-output-on-prefix
  @Output() loadUsers = new EventEmitter();
  currentUser: User = JSON.parse(localStorage.getItem('user'));
  likesParam: string;
  classId;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    this.userService
      .getUserWithLikees(this.authService.decodedToken.nameid)
      .subscribe((res: UserResult<User>) => {
        this.currentUserLikees = res.result.likees;
      });
  }

  getCurrentUserLikees(userId: string) {
    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < this.currentUserLikees.length; i++) {
      // console.log(this.currentUserLikees[i].likeeId + '|' + userId);
      if (this.currentUserLikees[i].likeeId === userId) {
        return true;
      }
    }
  }

  sendLike(id: number) {
    this.userService
      .sendLike(this.authService.decodedToken.nameid, id)
      .subscribe(
        data => {
          this.alertify.success('You have liked: ' + this.user.knownAs);
          this.loadUsers.emit();
        },
        error => {
          this.alertify.error(error);
        }
      );
  }

  unLike(id: number, classIdReceived: number) {
    this.userService.unLike(this.authService.decodedToken.nameid, id).subscribe(
      data => {
        this.alertify.success('You have unliked: ' + this.user.knownAs);
        this.loadUsers.emit();
      },
      error => {
        this.alertify.error(error);
      }
    );
  }
}
