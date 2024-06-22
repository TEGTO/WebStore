import { createAction, props } from "@ngrx/store";
import { UserData } from "../../index";

export const signInnUser = createAction(
    '[Auth] Sing In By User Data',
    props<{ userData: UserData }>()
);
export const signInnUserSuccess = createAction(
    '[Auth] Sing In By User Data Success',
    props<{ userData: UserData }>()
);
export const signInnUserFailure = createAction(
    '[Auth] Sing In By User Data Failure',
    props<{ error: any }>()
);

export const signUpUser = createAction(
    '[Auth] Sing Up User Data',
    props<{ userData: UserData }>()
);
export const signUpUserSuccess = createAction(
    '[Auth] Sing Up User Data Success',
    props<{ userData: UserData }>()
);
export const signUpUserFailure = createAction(
    '[Auth] Sing In User Data Failure',
    props<{ error: any }>()
);