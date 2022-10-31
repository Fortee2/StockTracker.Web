import { configureStore } from '@reduxjs/toolkit';
import securitiesReducer from '../features/securities/securitiesSlice';
import symbolsReducer from '../features/securities/SymbolsSlice';

export default configureStore({
  reducer: {
    securities: securitiesReducer,
    symbols: symbolsReducer,
  },
})