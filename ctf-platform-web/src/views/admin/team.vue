<template>


    <div class="common-layout">
        <el-container>
            <el-aside width="30%" style="background-color: greenyellow;">左侧</el-aside>
            <el-main >
                <CreateTeam></CreateTeam>
            </el-main>
            <el-aside width="30%" style="background-color: greenyellow;">右侧</el-aside>
        </el-container>
    </div>
</template>

<script setup lang="ts">
import CreateTeam from '@/components/team/createTeam.vue';


</script>

<style scoped></style>