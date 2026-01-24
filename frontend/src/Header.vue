<script lang="ts" setup>

const emits = defineEmits<{
    open: [targetName:string]
}>();
</script>

<template>
    <div class="header">
        <div class="title">TITLETITLE</div>
        <ul class="toolbar-navigation">
            <li>sd</li>
            <li>sd</li>
            <li>sd</li>
            <button class="appButton" @click="()=>{$emit(`open`, 'wordle')}">WORDLE</button>
        </ul>
        <div class="separator"></div>
        <div>some image</div>assf
        <div>Hamburger</div>
    </div>
</template>

<style scoped>
.toolbar-navigation{
    display:flex;
    align-items: center;
    justify-content: center;
}

.appButton{
    padding: 6px 10px;
    font-family: 'Courier New', Courier, monospace;
    font-weight: 700;
}

.header {
    padding: 16px;
    background-color: var(--c-primary);
    color: var(--c-light);
    display: flex;
    align-items: center;
    gap: 32px;
}

ul {
    list-style: none;
    display: flex;
    font-size: 20px;
    margin: 0;
    gap: 32px;
}

.title {
    font-size: 28px;
}
</style>
