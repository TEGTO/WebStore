import { createReducer, on } from "@ngrx/store";
import { getAuthUserDataSuccess, logOutUserSuccess, registerFailure, registerSuccess, registerUser, signInUser, signInUserFailure, signInUserSuccess, updateUserDataFailure, updateUserDataSuccess } from "../..";

export interface RegistrationState {
    isSuccess: boolean,
    error: any
}
const initialRegistrationState: RegistrationState = {
    isSuccess: true,
    error: null
};

export const registrationReducer = createReducer(
    initialRegistrationState,
    on(registerUser, (state) => ({
        ...state,
        isSuccess: false,
        error: null
    })),
    on(registerSuccess, (state) => ({
        ...state,
        isSuccess: true,
        error: null
    })),
    on(registerFailure, (state, { error: error }) => ({
        ...state,
        isSuccess: false,
        error: error
    })),
);

export interface AuthState {
    isAuthenticated: boolean,
    authToken: string,
    expiredOn: Date,
    userEmail: string,
    error: any
}
const initialAuthState: AuthState = {
    isAuthenticated: false,
    authToken: "",
    expiredOn: new Date(),
    userEmail: "",
    error: null
};
export interface UserAuthData {
    isAuthenticated: boolean,
    authToken: string,
    expiredOn: Date,
    userEmail: string
}
export const authReducer = createReducer(
    initialAuthState,
    on(signInUser, (state) => ({
        ...initialAuthState
    })),
    on(signInUserSuccess, (state, { userAuthData: userAuthData }) => ({
        ...state,
        isAuthenticated: true,
        authToken: userAuthData.authToken,
        expiredOn: userAuthData.expiredOn,
        userEmail: userAuthData.userEmail,
        error: null
    })),
    on(signInUserFailure, (state, { error: error }) => ({
        ...initialAuthState,
        error: error
    })),
    on(getAuthUserDataSuccess, (state, { userAuthData: userAuthData }) => ({
        ...state,
        isAuthenticated: userAuthData.isAuthenticated,
        authToken: userAuthData.authToken,
        expiredOn: userAuthData.expiredOn,
        userEmail: userAuthData.userEmail,
        error: null
    })),
    on(logOutUserSuccess, (state) => ({
        ...initialAuthState,
    })),

    on(updateUserDataSuccess, (state, { userUpdateData: userUpdateData }) => ({
        ...state,
        userEmail: userUpdateData.newEmail ? userUpdateData.newEmail : userUpdateData.oldEmail,
        error: null
    })),
    on(updateUserDataFailure, (state, { error: error }) => ({
        ...state,
        error: error
    })),
);