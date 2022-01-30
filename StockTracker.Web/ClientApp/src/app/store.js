import { configureStore } from '@reduxjs/toolkit';
import securitiesReducer from '../features/securities/securitiesSlice';

export default configureStore({
  reducer: {
    securities: securitiesReducer,
  },
})