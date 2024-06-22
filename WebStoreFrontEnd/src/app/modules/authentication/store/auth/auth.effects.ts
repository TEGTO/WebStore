import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, map, mergeMap, of } from "rxjs";
import { registerFailure, registerSuccess, registerUser } from "../..";
import { AuthenticationApiService } from "../../../shared";

@Injectable()
export class RegistrationEffects {
    constructor(private actions$: Actions,
        private apiService: AuthenticationApiService) { }

    registerUser$ = createEffect(() =>
        this.actions$.pipe(
            ofType(registerUser),
            mergeMap((action) =>
                this.apiService.registerUser(action.userRegistrationData).pipe(
                    map(() => registerSuccess()),
                    catchError(error => of(registerFailure({ error: error.message })))
                )
            )
        )
    );
}