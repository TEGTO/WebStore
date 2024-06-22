import { createReducer, on } from "@ngrx/store";
import { registerFailure, registerSuccess, registerUser } from "../..";

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