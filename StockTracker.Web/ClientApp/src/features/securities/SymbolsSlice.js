import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";

export const getSymbols = createAsyncThunk("securities/symbols",
    async () => {
        const response = await axios.get(
          "api/security/"
        );
        return response.data;
      }
)

export const symbolSlice = createSlice(
    {
        name: "symbols",
        initialState: {
            data: [],
            loading: "idle",
            error: null
        },
        reducers: {},
        extraReducers: (builder) => {
            builder.addCase(getSymbols.pending, (state, action) => {
                if (state.loading === "idle") {
                    state.loading = "pending";
                }
            });

            builder.addCase(getSymbols.fulfilled, (state, action) => {
                if (state.loading === "pending") {
                    state.loading = "loaded";
                    state.data = action.payload;
                }
            });

            builder.addCase(getSymbols.rejected, (state, action) => {
                if (state.loading === "pending") {
                    state.loading = "idle";
                    state.error = action.error;
                }
            });
        }
    }
);

export const { toogleLoading, toggleEditor, updateSecurities } = symbolSlice.actions

export default symbolSlice.reducer