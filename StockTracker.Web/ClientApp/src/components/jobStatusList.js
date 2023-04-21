import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { fetchJobStatus } from '../features/utilities/jobStatusAction';

const JobStatusList = () => {
    const dispatch = useDispatch();
    const jobStatuses = useSelector((state) => state.jobStatus.jobStatuses);
    const loading = useSelector((state) => state.jobStatus.loading);
    const error = useSelector((state) => state.jobStatus.error);

    useEffect(() => {
        dispatch(fetchJobStatus());
    }, [dispatch]);

    return (
        <div>
            <h2>Job Status List</h2>
            {loading && <p>Loading...</p>}
            {error && <p>Error: {error}</p>}
            <ul>
                {jobStatuses.map((jobStatus) => (
                    <li key={jobStatus.id}>
                        <div>
                            <strong>Job Name:</strong> {jobStatus.jobName}
                        </div>
                        <div>
                            <strong>Activity Time:</strong> {new Date(jobStatus.activityTime).toLocaleString()}
                        </div>
                        <div>
                            <strong>Activity Description:</strong> {jobStatus.activityDescription}
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default JobStatusList;

