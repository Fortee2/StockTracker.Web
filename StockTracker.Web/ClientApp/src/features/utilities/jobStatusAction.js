import axios from 'axios';

export const FETCH_JOB_STATUS_START = 'FETCH_JOB_STATUS_START';
export const FETCH_JOB_STATUS_SUCCESS = 'FETCH_JOB_STATUS_SUCCESS';
export const FETCH_JOB_STATUS_FAILURE = 'FETCH_JOB_STATUS_FAILURE';

export const fetchJobStatus = () => {
    return async (dispatch) => {
        dispatch({ type: FETCH_JOB_STATUS_START });
        try {
            const response = await axios.get('api/jobstatus');
            dispatch({ type: FETCH_JOB_STATUS_SUCCESS, payload: response.data });
        } catch (error) {
            dispatch({ type: FETCH_JOB_STATUS_FAILURE, payload: error.message });
        }
    };
};