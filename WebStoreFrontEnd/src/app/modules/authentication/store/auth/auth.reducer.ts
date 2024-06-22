
export interface AuthState {
    boards: Board[];
    error: any;
}
const initialState: AuthState = {
    boards: [],
    error: null
};