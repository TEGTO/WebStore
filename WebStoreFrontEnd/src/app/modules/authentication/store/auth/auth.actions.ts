import { createAction, props } from "@ngrx/store";
import { UserRegistrationDto } from "../../../shared";

// export const signInnUser = createAction(
//     '[Auth] Sing In By User Data',
//     props<{ userData: UserData }>()
// );
// export const signInnUserSuccess = createAction(
//     '[Auth] Sing In By User Data Success',
//     props<{ userData: UserData }>()
// );
// export const signInnUserFailure = createAction(
//     '[Auth] Sing In By User Data Failure',
//     props<{ error: any }>()
// );

export const registerUser = createAction(
    '[Auth] Register New User',
    props<{ userRegistrationData: UserRegistrationDto }>()
);
export const registerSuccess = createAction(
    '[Auth] Register New User Success'
);
export const registerFailure = createAction(
    '[Auth] Register New User Failure',
    props<{ error: any }>()
);