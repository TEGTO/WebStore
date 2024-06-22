import { createFeatureSelector, createSelector } from "@ngrx/store";
import { RegistrationState } from "../..";


export const selectRegistrationState = createFeatureSelector<RegistrationState>('registration');

export const selectIsRegistrationSuccess = createSelector(
    selectRegistrationState,
    (state: RegistrationState) => state.isSuccess
);
export const selectRegistrationErrors = createSelector(
    selectRegistrationState,
    (state: RegistrationState) => state.error
);
