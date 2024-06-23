import { createAction, props } from "@ngrx/store";
import { UserAuthData } from "../..";
import { UserAuthenticationDto, UserRegistrationDto, UserUpdateDataDto } from "../../../shared";

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

export const signInUser = createAction(
    '[Auth] Sing In By User Data',
    props<{ userAuthData: UserAuthenticationDto }>()
);
export const signInUserSuccess = createAction(
    '[Auth] Sing In By User Data Success',
    props<{ userAuthData: UserAuthData }>()
);
export const signInUserFailure = createAction(
    '[Auth] Sing In By User Data Failure',
    props<{ error: any }>()
);
export const getAuthUserData = createAction(
    '[Auth] Get Authenticated User Data'
);
export const getAuthUserDataSuccess = createAction(
    '[Auth] Get Authenticated User Data Success',
    props<{ userAuthData: UserAuthData }>()
);
export const logOutUser = createAction(
    '[Auth] Log out Authenticated User'
);
export const logOutUserSuccess = createAction(
    '[Auth] Log out Authenticated User Success'
);
export const updateUserData = createAction(
    '[Auth] Update Authenticated User',
    props<{ userUpdateData: UserUpdateDataDto }>()
);
export const updateUserDataSuccess = createAction(
    '[Auth] Update Authenticated User Success',
    props<{ userUpdateData: UserUpdateDataDto }>()
);
export const updateUserDataFailure = createAction(
    '[Auth] Update Authenticated User Failure',
    props<{ error: any }>()
);