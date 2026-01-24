<script lang="ts" setup>
import { ref } from 'vue';


const rowsCount = 5;
const tilesPerRow = 5;

var currentWord = '';
var submittedWords: Array<string> = [];

const currentRow = ref(0);
const currentColumn = ref(0);
const solution: string = "world"

function GetLetterAt(r_index: number, c_index: number) {
    if (r_index < currentRow.value)
        return submittedWords[r_index]?.charAt(c_index) ?? '';

    return currentWord[c_index] ?? '';
}

function GetTileClasses(r_index: number, c_index: number) {
    const classes = [];
    const submitted = r_index < currentRow.value;
    if (submitted) {
        const guess: string = GetLetterAt(r_index, c_index);
        // either dim as a miss, yellow as a hint or green as a success
        if (guess == solution.charAt(c_index))
            classes.push('success')

        if (solution.includes(guess))
            classes.push('hint')

        classes.push('miss')

        return classes;
    }

    if (c_index < currentColumn.value)
        classes.push('typed')

    if (c_index == currentColumn.value)
        classes.push('typing')

    return classes;
}

function processKey(event: KeyboardEvent) {
    console.log(currentRow.value, currentColumn.value, currentWord);


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

        console.log(currentRow.value, currentColumn.value, currentWord);

    } else if (event.key == "Enter") {
        if (currentWord.length != 5)
            return;

        submittedWords.push(currentWord);
        currentWord = '';
        currentColumn.value = 0;
        currentRow.value += 1;
    }

    return;
}

</script>

<template>
    <Teleport to="#overlay">
        <div class="container" ref="wordleContainer" tabindex="0" @keydown="processKey">
            <div class = "cover">
                <div class="modalTop">
                    <button class="modalButton closeButtonContainer">CLOSE</button>
                </div>
                <div class="modalBottom">
                    <button class="modalButton hint">HINT</button>
                    <button class="modalButton guess">GUESS</button>
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
    justify-content: end;
    top:25px;
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

.modalButton.hint{
    position: relative;
    right: 8px;
    background-color: var(--tmp-yellow);
}

.modalButton.guess{
    position: relative;
    right: 8px;
    color:var(--c-dark);
    background-color: var(--c-light);
    padding:auto 220px;
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
