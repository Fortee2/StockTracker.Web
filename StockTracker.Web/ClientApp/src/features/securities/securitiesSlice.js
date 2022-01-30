import { createSlice } from "@reduxjs/toolkit";

export const securitiesSlice = createSlice(
    {
        name: "securities",
        initialState: {
            editorVisible:false,
            isListLoading: true,
            securitiesData:[],
        },
        reducers:{
            toogleLoading: (state) =>{
                state.isListLoading = !state.isListLoading;
                console.log('toggleLoading');
            },
            toggleEditor: (state) => {
                state.editorVisible = !state.editorVisible;
                console.log('toggleEditor');
            },
            updateSecurities: (state, action) => {
                state.securitiesData = action.payload;
                console.log('updateSecurities');
            }
        }
    }
);

export const { toogleLoading, toggleEditor, updateSecurities} = securitiesSlice.actions

export default securitiesSlice.reducer