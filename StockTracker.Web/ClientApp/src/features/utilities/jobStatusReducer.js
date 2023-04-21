import { FETCH_JOB_STATUS_START, FETCH_JOB_STATUS_SUCCESS, FETCH_JOB_STATUS_FAILURE } from 'jobStatusActions';

const initialState = {
    jobStatuses: [],
    loading: false,
    error: null
};

const jobStatusReducer = (state = initialState, action) => {
    switch (action.type) {
        case FETCH_JOB_STATUS_START:
            return { ...state, loading: true, error: null };
        case FETCH_JOB_STATUS_SUCCESS:
            return { ...state, loading: false, jobStatuses: action.payload };
        case FETCH_JOB_STATUS_FAILURE:
            return { ...state, loading: false, error: action.payload };
        default:
            return state;
    }
};

export default jobStatusReducer;
