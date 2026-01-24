<script lang="ts" setup>
import Header from './Header.vue';
import Footer from './Footer.vue'
import Wordle from './Wordle.vue';
import { ref } from 'vue';

const shouldBlur = ref(false);
const blurSize = ref(2);

</script>

<template>
  <div id="overlay"></div>
  <div class="page" >
    <Header />

    <div class="content">
      <Wordle/>
    </div>

    <Footer />
  </div>
</template>

<style>
#overlay{
  position:fixed;
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 900;
  width: 100vw;
  height: 100vh;
}

.separator {
  flex: 1;
}

.page {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  background-color: aliceblue;
}

.content {
  flex-grow: 1;
  display:flex;
  justify-content:center;
  align-items:center;
}
</style>
