<script lang="ts" setup>
import axios, { HttpStatusCode } from 'axios';
import { nextTick, onMounted, reactive, ref } from 'vue';

const emits = defineEmits<{
    closeModal:[];
}>();

const rowsCount = 5;
const tilesPerRow = 5;

var currentWord = '';
var submittedWords: string[]= reactive([]); 
var isWin = ref(false);

const currentRow = ref(0);
const currentColumn = ref(0);
var solution = ref("")

const wordleContainer = ref<HTMLElement|null>(null);

onMounted(()=>resetSolution());

async function resetSolution(){
    var solutionData = await axios.post('http://localhost:5124/api/words/random',{});
    if(solutionData.status==HttpStatusCode.Ok){
        solution.value = solutionData.data['word'];
    }
}



function GetLetterAt(r_index: number, c_index: number) {
    if (r_index < currentRow.value)
        return submittedWords[r_index]?.charAt(c_index) ?? '';

    return currentWord[c_index] ?? '';
}

function GetTileClasses(r_index: number, c_index: number) {
    const classes = [];
    const submitted = r_index < submittedWords.length;
    if (submitted) {
        const guess: string = GetLetterAt(r_index, c_index);
        // either dim as a miss, yellow as a hint or green as a success
        if (guess == solution.value.charAt(c_index)){
            classes.push('success')
        }
        else if (solution.value.includes(guess)){
            classes.push('hint')
        }
        else{
            classes.push('miss')
        }

        return classes;
    }

    if (c_index < currentColumn.value)
        classes.push('typed')

    if (c_index == currentColumn.value)
        classes.push('typing')

    return classes;
}

function SubmitWord(){
    if (currentWord.length != 5)
            return;
    
    submittedWords.push(currentWord);

    if(currentWord == solution.value){
        isWin.value = true;
        nextTick(()=>setTimeout(()=>alert('you won'),0))
        //todo: handle a win 
        return;
    }

    currentWord = '';
    currentColumn.value = 0;
    currentRow.value += 1;
}

function processKey(event: KeyboardEvent) {

    if (event.type != 'keydown')
        return;

    if (event.key == "Backspace") {
        if (currentWord.length <= 0)
            return;

        currentWord = currentWord.substring(0, currentWord.length - 1);
        currentColumn.value--;
    }
    else if (event.key.length == 1) {

        if (currentWord.length >= 5)
            return;

        currentWord += event.key;
        currentColumn.value++;


    } else if (event.key == "Enter") {
        SubmitWord();
    }

    return;
}

function resetPuzzle(){
    submittedWords=[];
    currentColumn.value = 0;
    currentRow.value = 0;
    currentWord = '';
    isWin.value = false;
    setTimeout(()=>resetSolution(),0);
    nextTick(()=>{
        wordleContainer.value?.focus();
    });
}

</script>

<template>
    <Teleport to="#overlay">
        <div class="container" ref="wordleContainer" tabindex="0" @keydown="processKey">
            <div class = "cover">
                <div class="modalTop">
                    <button class="modalButton closeButtonContainer" @click="$emit('closeModal')">CLOSE</button>
                </div>
                <div class="modalBottom">
                    <div class="left">
                        <button class="modalButton reset" @click="resetPuzzle">
                            <img src="../public/reset.png"/>
                        </button>
                    </div>
                    <div class="right">
                        <button class="modalButton hint">HINT</button>
                        <button class="modalButton guess" @click="SubmitWord">GUESS</button>
                    </div>
                </div>
            </div>

            <div class="row" v-for="(_, r_index) in rowsCount" :class="{ invisible: r_index > currentRow }">
                <div class="tile caveat-brush-regular" v-for="(_, c_index) in tilesPerRow"
                    :key="r_index + ',' + c_index" :class="GetTileClasses(r_index, c_index)"
                    v-text="GetLetterAt(r_index, c_index)">
                </div>
            </div>
        </div>
    </Teleport>
</template>

<style lang="css" scoped>
.cover{
    position:absolute;
    display:flex;
    flex-direction: column;
    width: 100%;
    height: 100%;
    top: 0;
    left: 0;
}

.modalTop{
    position:relative;
    align-self: end;
    bottom:25px;
}

.modalBottom{
    position:relative;
    display:flex;
    margin-top:auto;
    top:25px;
}

.modalBottom .left{
    margin-right:auto;
    margin-left: 8px;
}

.modalBottom .right{
    margin-left:auto;
    margin-right: 8px;
    display:flex;
    gap:10px;
}

.modalButton{
    position:relative;
    align-items:center;
    justify-content:center;
    width: 106px;
    height:50px;
    border-style: solid;
    border-width: 5px;
    border-radius: 20px;
    font-family: 'Courier New', Courier, monospace;
    font-size: 24px;
    font-weight: 800;
    border-color: var(--c-secondary);
}

.modalButton.closeButtonContainer{
    position: relative;
    right:8px;
    background-color: var(--c-exit);
}
.modalButton.closeButtonContainer:hover{
    background-color: hsl(from var(--c-exit) h s calc(l * 0.75));
}
.modalButton.closeButtonContainer:active {
    background-color: hsl(from var(--c-exit) h s calc(l * 0.65));
}

.modalButton.hint{
    position: relative;
    
    background-color: var(--tmp-yellow);
}
.modalButton.hint:hover{
    background-color: hsl(from var(--tmp-yellow) h s calc(l * 0.75));
}
.modalButton.hint:active {
    background-color: hsl(from var(--tmp-yellow) h s calc(l * 0.65));
}

.modalButton.guess{
    position: relative;
    
    color:var(--c-dark);
    background-color: var(--c-light);
    padding:auto 220px;
}
.modalButton.guess:hover{
    background-color: hsl(from var(--c-light) h s calc(l * 0.85));
}
.modalButton.guess:active {
    background-color: hsl(from var(--c-light) h s calc(l * 0.75));
}

.modalButton.reset{
    width:50px;
    height:50px;
    padding:0px;
    display:flex;
    justify-content: center;
    align-items: center;
}

.modalButton.reset img{
    width:100%;
    height:100%;
    object-fit: contain;
}

.tile {
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: hsl(from var(--c-light) h s calc(l * 0.85));
    color: var(--c-dark);
    font-size: 36px;
    font-size: 36px;
    height: 64px;
    width: 64px;
    border-radius: 4px;
}

.invisible {
    opacity: 0;
}

.tile.miss {
    background-color: hsl(from var(--c-light) h s calc(l * 0.85));
    color: var(--c-dark);
}

.tile.hint {
    background-color: var(--tmp-yellow);
    color: var(--c-dark);
}

.tile.success {
    background-color: var(--c-secondary);
    color: var(--c-light);
}

.tile.typing {
    background-color: var(--c-light);
    color: var(--c-dark);
}

.container {
    position: fixed;
    display: flex;
    flex-direction: column;
    background-color: var(--c-primary);
    color: var(--c-light);
    padding: 35px;
    border-radius: 10px;
    border-color: var(--c-secondary);
    border-style: solid;
    border-width: 5px;
}

.row {
    display: flex;
    flex-direction: row;
}

.container,
.row {
    gap: 10px;
}
</style>
